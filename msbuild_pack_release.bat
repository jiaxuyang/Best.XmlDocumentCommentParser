dotnet restore Best.XmlDocumentCommentParser.sln
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\amd64\MSBuild.exe" /t:pack /p:Configuration=Release /p:IncludeSymbols=true;IncludeSource=true Best.XmlDocumentCommentParser.sln

MKDIR pkg
MKDIR symbols

move /Y .\Best.XmlDocumentCommentParser\bin\Release\*.symbols.nupkg .\symbols\

move /Y .\Best.XmlDocumentCommentParser\bin\Release\*.nupkg .\pkg\