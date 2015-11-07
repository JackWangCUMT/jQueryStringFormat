## jQueryStringFormat
C#  String Interpolation Using jQuery $
## Usage
```C#
string Name = "JackWang";
int ID = 200;
string template = "exec func($Name,$$ID)";
template.jQueryStringFormat(template, new{ID,Name});//"exec func(JackWang,$200)"
```
