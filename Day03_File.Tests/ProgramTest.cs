using System;
using System.IO;
using Day03_File;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day03_File.Tests;

[TestClass]
[TestSubject(typeof(Program))]
public class ProgramTest
{

    [TestMethod]
    public void CopyFile()
    {
        string sourcePath = Path.GetTempFileName();
        string destPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");
        string expectedText = "Hello, C# Unit Test!";
        
        File.WriteAllText(sourcePath, expectedText);
        IFileCopier copier = new DefaultFileCopier();

        try
        {
            copier.CopyFile(sourcePath, destPath);
            Assert.IsTrue(File.Exists(destPath),"파일 생성되지 않았음");

            string actualText = File.ReadAllText(destPath);
            Assert.AreEqual(expectedText,actualText,"원본과 카피가 일치하지 않음");
        }
        finally
        {
            if(File.Exists(sourcePath)) File.Delete(sourcePath);
            if(File.Exists(destPath)) File.Delete(destPath);
        }
    }
}