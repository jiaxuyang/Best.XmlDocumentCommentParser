dotnet restore Best.XmlDocumentCommentParser.sln
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\amd64\MSBuild.exe" /t:pack /p:Configuration=Debug /p:IncludeSymbols=true;IncludeSource=true Best.XmlDocumentCommentParser.sln

MKDIR pkg
MKDIR symbols

move /Y .\Best.XmlDocumentCommentParser\bin\Debug\*.symbols.nupkg .\symbols\

move /Y .\Best.XmlDocumentCommentParser\bin\Debug\*.nupkg .\pkg\