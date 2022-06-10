const string WORKDIR = @"C:\Users\TheLeftExit\Desktop\Color Theory\Patreon Exclusives";
const string FFMPEG = @"C:\ffmpeg\ffmpeg.exe";

Directory.SetCurrentDirectory(WORKDIR);
foreach(string mp3Name in Directory.GetFiles(".", "*.mp3")) {
    var wavName = Path.ChangeExtension(mp3Name, ".wav");
    var flacName = Path.ChangeExtension(mp3Name, ".flac");
    var txtName = Path.ChangeExtension(mp3Name, ".txt");
    if (!File.Exists(wavName)) throw new ApplicationException($"Could not match {mp3Name} to a WAV file.");
    
    //var cmdArgs = $"-i \"{mp3Name}\" -i \"{wavName}\" \"{flacName}\"";
    var cmdArgs1 = $"-i \"{mp3Name}\" -f ffmetadata \"{txtName}\"";
    var cmdArgs2 = $"-f ffmetadata -i \"{txtName}\" -i \"{wavName}\" \"{flacName}\"";
    await System.Diagnostics.Process.Start(FFMPEG, cmdArgs1).WaitForExitAsync();
    await System.Diagnostics.Process.Start(FFMPEG, cmdArgs2).WaitForExitAsync();
    File.Delete(txtName);
}