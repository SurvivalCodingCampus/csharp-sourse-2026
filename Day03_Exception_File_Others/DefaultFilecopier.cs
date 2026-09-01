namespace Day03_예외파일조작데이터식;

public class DefaultFilecopier
{
    private string text;
    public DefaultFilecopier(string sourceFilePath, string destinationFilePath)
    {
        if (string.IsNullOrWhiteSpace(sourceFilePath))
        {
            throw new ArgumentException("원본 파일 경로가 비어 있습니다.", nameof(sourceFilePath));
        }

        if (string.IsNullOrWhiteSpace(destinationFilePath))
        {
            throw new ArgumentException("복사할 파일 경로가 비어 있습니다.", nameof(destinationFilePath));
        }

        if (!File.Exists(sourceFilePath))
        {
            throw new FileNotFoundException("원본 파일을 찾을 수 없습니다.", sourceFilePath);
        }

        // false: 같은 이름의 파일이 이미 있으면 예외 발생
        File.Copy(sourceFilePath, destinationFilePath, overwrite: false);
    }
}