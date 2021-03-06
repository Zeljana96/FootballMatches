#pragma checksum "C:\Projekti\WaySevenTasks\Task5\Views\Match\Statistics.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4888d05c186bf2466304f7f263ecfd69c37e0ba4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Match_Statistics), @"mvc.1.0.view", @"/Views/Match/Statistics.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Projekti\WaySevenTasks\Task5\Views\_ViewImports.cshtml"
using Task5;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Projekti\WaySevenTasks\Task5\Views\_ViewImports.cshtml"
using Task5.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4888d05c186bf2466304f7f263ecfd69c37e0ba4", @"/Views/Match/Statistics.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8ed131d58bd4b5036a37624c2a7f4116adf8e171", @"/Views/_ViewImports.cshtml")]
    public class Views_Match_Statistics : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Task5.Models.TeamPlayer>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Projekti\WaySevenTasks\Task5\Views\Match\Statistics.cshtml"
  
    ViewData["Title"] = "Create";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<br /><br />

<table class=""table table-bordered table-striped"" style=""width:100%"">
    <thead>
        <tr>
            <th>
               Team
            </th>
            <th>
               Number of wins
            </th>
            <th>
               Number of draws
            </th>
            <th>
               Number of loses
            </th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 26 "C:\Projekti\WaySevenTasks\Task5\Views\Match\Statistics.cshtml"
         foreach (var obj in ViewBag.TeamStatistics)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td width=\"20%\">");
#nullable restore
#line 29 "C:\Projekti\WaySevenTasks\Task5\Views\Match\Statistics.cshtml"
                           Write(obj.Split("/")[0]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td width=\"10%\">");
#nullable restore
#line 30 "C:\Projekti\WaySevenTasks\Task5\Views\Match\Statistics.cshtml"
                           Write(obj.Split("/")[1]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td width=\"10%\">");
#nullable restore
#line 31 "C:\Projekti\WaySevenTasks\Task5\Views\Match\Statistics.cshtml"
                           Write(obj.Split("/")[2]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td width=\"10%\">");
#nullable restore
#line 32 "C:\Projekti\WaySevenTasks\Task5\Views\Match\Statistics.cshtml"
                           Write(obj.Split("/")[3]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 34 "C:\Projekti\WaySevenTasks\Task5\Views\Match\Statistics.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </tbody>
</table>

<br /><br />

<table class=""table table-bordered table-striped"" style=""width:100%"">
    <thead>
        <tr>
            <th>
               Player
            </th>
            <th>
               Matches
            </th>
            <th>
               Goals
            </th>
            <th>
               Team
            </th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 58 "C:\Projekti\WaySevenTasks\Task5\Views\Match\Statistics.cshtml"
         foreach (var obj in ViewBag.PlayerStatistics)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td width=\"20%\">");
#nullable restore
#line 61 "C:\Projekti\WaySevenTasks\Task5\Views\Match\Statistics.cshtml"
                           Write(obj.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td width=\"10%\">");
#nullable restore
#line 62 "C:\Projekti\WaySevenTasks\Task5\Views\Match\Statistics.cshtml"
                           Write(obj.Matches);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td width=\"10%\">");
#nullable restore
#line 63 "C:\Projekti\WaySevenTasks\Task5\Views\Match\Statistics.cshtml"
                           Write(obj.Goals);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td width=\"10%\">");
#nullable restore
#line 64 "C:\Projekti\WaySevenTasks\Task5\Views\Match\Statistics.cshtml"
                           Write(obj.Team.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 66 "C:\Projekti\WaySevenTasks\Task5\Views\Match\Statistics.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Task5.Models.TeamPlayer> Html { get; private set; }
    }
}
#pragma warning restore 1591
