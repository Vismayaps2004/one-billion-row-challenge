namespace OneBillionRowChallenge;

using System.IO;
using System;
public class WeatherRecordReader : IDisposable
{
    private readonly FileStream stream;
    private readonly byte[] buffer = new byte[1024 * 1024];
    
    private int bytesInBuffer;
    private int currentIndex;
    private int lineStart;
    
    public WeatherRecordReader(string filePath)
    {
        stream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read
            );
    }

    public bool  TryReadRecord(out ReadOnlySpan<byte> record)
    {
        while (true)
        {
            record = default;
            if (currentIndex >= bytesInBuffer)
            {
                bytesInBuffer = stream.Read(buffer);

                if (bytesInBuffer == 0)
                {
                    return false;
                }
                ResetParserState(0);
            }
            if (parseBytes(out record))
                return true;
            if (!readRemaining(out record))
                return false;
        }
    }

    private bool parseBytes(out ReadOnlySpan<byte> record)
    {
        
        for (int i = currentIndex; i < bytesInBuffer; i++)
        {
            if (buffer[i] == (byte)'\n')
            {
                record = buffer.AsSpan(lineStart, i - lineStart);
                ResetParserState(i + 1);
                return true;
            }
        }
        currentIndex = bytesInBuffer;
        record = default;
        return false;
    }

    private bool readRemaining(out ReadOnlySpan<byte> record)
    {
        int remainingBytes = bytesInBuffer - lineStart;
        if (remainingBytes > 0)
        {
            buffer.AsSpan(lineStart, remainingBytes)
                .CopyTo(buffer);
        }
        
        int bytesRead = stream.Read(buffer.AsSpan(remainingBytes));
        bytesInBuffer = remainingBytes + bytesRead;
        
        if (bytesRead == 0) {
            if (remainingBytes > 0) {
                buffer.AsSpan(lineStart, remainingBytes)
                    .CopyTo(buffer);
                
                record = buffer.AsSpan(0, remainingBytes);
                bytesInBuffer = 0;
                return true;
            }

            record = default;
            return false;
        }
        ResetParserState(0);

        record = default;
        return true;
    }

    private void ResetParserState(int resetValue)
    {
        currentIndex = resetValue;
        lineStart = resetValue;
    }

    public void Dispose()
    {
        stream.Dispose();
    }
}