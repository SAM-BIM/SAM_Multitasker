using SAM.Core;

int index = SAM.Core.Query.IndexOf("abcd", 'c');

var json = "{\"name\":\"John\", \"age\":30}";
dynamic person = System.Text.Json.Nodes.JsonNode.Parse(json);

int i = 1;
int j = 2;

int a = i + j;

System.IO.File.WriteAllText(@"C:\Users\jakub\Downloads\New folder\aaa.txt", a.ToString());

return index;