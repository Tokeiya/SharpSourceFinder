<Query Kind="Statements">
  <NuGetReference>Microsoft.CodeAnalysis.CSharp</NuGetReference>
  <Namespace>Microsoft.CodeAnalysis</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp.Syntax</Namespace>
</Query>

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));

var hoge=CSharpSyntaxTree.ParseText(File.ReadAllText("ParseSample.cs"));
