namespace OneBillionRowChallenge;

using System.IO;
using System;
public class WeatherRecordReader : IDisposable, IAsyncDisposable
{
    private FileStream stream;
    private byte[] buffer = new byte[1024 * 1024];
    private int bytesInBuffer;
    private int currentIndex;
    private int lineStart;
    
    public WeatherRecordReader(string filePath)
    {
         stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
    }

    public bool TryReadRecord(out ReadOnlySpan<byte> record)
    {
        while (true)
        {
            record = default;
            if (currentIndex >= bytesInBuffer)
            {
                bytesInBuffer = stream.Read(buffer);
                if (bytesInBuffer == 0)
                    return false;
                ResetParserState(0);
            }
            if (ParseBytes(out record))
                return true;
                
            if (!ReadRemaining(out record))
                return false;
        }
    }

    private bool ReadRemaining(out ReadOnlySpan<byte> record)
    {
        record = default;
        int remainingBytes = bytesInBuffer - lineStart;
        if (remainingBytes > 0)
        {
            buffer.AsSpan(lineStart, remainingBytes)
                .CopyTo(buffer);
        }
        int bytesRead = stream.Read(buffer.AsSpan(remainingBytes));
        bytesInBuffer = remainingBytes + bytesRead;

        if (bytesRead == 0)
        {
            if (remainingBytes > 0)
            {
                record = buffer.AsSpan(0, remainingBytes);
                bytesInBuffer = 0;
                return true;
            }
            return false;
        }
        ResetParserState(0);
        return true;
    }

    private bool ParseBytes(out ReadOnlySpan<byte> record)
    {
        record = default;
        for (int i = currentIndex; i < bytesInBuffer; i++)
        {
            if (buffer[i] == (byte) '\n')
            { 
                record = buffer.AsSpan(lineStart, i - lineStart);
                ResetParserState(i + 1);
                return true;
            }
        }
        currentIndex = bytesInBuffer;
        return false;
    }
    
    private void ResetParserState(int resetValue)
    {
        lineStart = resetValue;
        currentIndex = resetValue;
    }

    public void Dispose()
    {
        stream.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await stream.DisposeAsync();
    }
}