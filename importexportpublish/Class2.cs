using System;

public class Class1
{
    static void Main(string[] args)
    {
        Run();
    }
    public Class1()
    { }

          public bool Run()
    {
        textBoxResults.Text = "Starting to process zip files in dir " + textBoxDir.Text + Environment.NewLine;
        DirectoryInfo ourDir = new DirectoryInfo(textBoxDir.Text);
        FileInfo[] ourZipFiles = ourDir.GetFiles("*.zip");
        int noFiles = ourZipFiles.Length;
        textBoxResults.Text += "Found " + noFiles + " zip files to extract:" + Environment.NewLine;
        for (int y = 0; y < ourZipFiles.Length; y++)
        {
            textBoxResults.Text += "Extracting " + ourZipFiles[y].FullName + ".. ";
            //extract contents
            //give us a zip rep
            try
            {
                ZipFile zf = new ZipFile(ourZipFiles[y].FullName);
                //get first entry and extract it..
                foreach (ZipEntry ze in zf)
                {
                    if (ze.CanDecompress && ze.IsFile)
                    {
                        //ok. we can extract something. lets do it then. set up fastzip to do it..
                        FastZip fz = new FastZip();
                        fz.ExtractZip(zf.Name, textBoxOutDir.Text, ze.Name);
                        FileInfo tmpfi = new FileInfo(textBoxOutDir.Text + ze.Name);
                        //change file extension
                        string full = tmpfi.FullName;
                        //textBoxResults.Text += "FULL: " + full + Environment.NewLine + " short: " +  + Environment.NewLine;
                        string newFileName = tmpfi.Directory.FullName + "" + ourZipFiles[y].Name.Substring(0, ourZipFiles[y].Name.Length - 7) + tmpfi.Name.Substring(tmpfi.Name.Length - 3);
                        //rename
                        try
                        {
                            //  textBoxResults.Text += "from filename " + tmpfi.FullName + " to " +newFileName;
                            tmpfi.MoveTo(newFileName);
                        }
                        catch (IOException ioex)
                        {
                            throw new ZipException("can’t move file; extracted file not found " + ioex.Message);
                        }
                        textBoxResults.Text += ze.Name + " (renamed to " + newFileName + ") ";
                        break; //break iteration after first valid zip entry. for now we only want the first one..
                    }
                }
                textBoxResults.Text += "..done :) " + Environment.NewLine;
            }
            catch (ZipException zex)
            {
                textBoxResults.Text += "..failed :( ERROR CAUSE: " + zex.Message + Environment.NewLine;
            }

        }
    }
}

