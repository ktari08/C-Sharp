#pragma checksum "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\Home\Dashboard.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2975e8de1ad924b11da3c08cdf262b3d182b3e7c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Dashboard), @"mvc.1.0.view", @"/Views/Home/Dashboard.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Dashboard.cshtml", typeof(AspNetCore.Views_Home_Dashboard))]
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
#line 1 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\_ViewImports.cshtml"
using wedding_planner;

#line default
#line hidden
#line 2 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\_ViewImports.cshtml"
using wedding_planner.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2975e8de1ad924b11da3c08cdf262b3d182b3e7c", @"/Views/Home/Dashboard.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e0aadd694686a14c367f0150bd67bec2e71f0b6a", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Dashboard : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 35, true);
            WriteLiteral("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n");
            EndContext();
            BeginContext(35, 214, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2975e8de1ad924b11da3c08cdf262b3d182b3e7c3396", async() => {
                BeginContext(41, 201, true);
                WriteLiteral("\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"ie=edge\">\r\n    <title>Wedding Planner</title>\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(249, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(251, 1941, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2975e8de1ad924b11da3c08cdf262b3d182b3e7c4796", async() => {
                BeginContext(257, 489, true);
                WriteLiteral(@"
    <h1>Welcome to the Wedding Planner</h1>
    <a href=""logout"">Logout</a> | <a href=""/new"">New Wedding</a>
    <table>
        <tr style=""border: 1px solid black"">
            <th style=""padding: 5px; border: 1px solid black;"">Wedding</th>
            <th style=""padding: 5px; border: 1px solid black;"">Date</th>
            <th style=""padding: 5px; border: 1px solid black;"">Guests</th>
            <th style=""padding: 5px; border: 1px solid black;"">Action</th>
        </tr
");
                EndContext();
#line 19 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\Home\Dashboard.cshtml"
             foreach(Wedding wedding in @ViewBag.AllWeddings)
            {

#line default
#line hidden
                BeginContext(824, 121, true);
                WriteLiteral("                <tr>\r\n                    <td style=\"padding: 5px; border: 1px solid black;\">\r\n                        <a");
                EndContext();
                BeginWriteAttribute("href", " href=\"", 945, "\"", 983, 2);
                WriteAttributeValue("", 952, "/viewwedding/", 952, 13, true);
#line 23 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\Home\Dashboard.cshtml"
WriteAttributeValue("", 965, wedding.WeddingId, 965, 18, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(984, 31, true);
                WriteLiteral(">\r\n                            ");
                EndContext();
                BeginContext(1016, 13, false);
#line 24 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\Home\Dashboard.cshtml"
                       Write(wedding.groom);

#line default
#line hidden
                EndContext();
                BeginContext(1029, 3, true);
                WriteLiteral(" & ");
                EndContext();
                BeginContext(1033, 13, false);
#line 24 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\Home\Dashboard.cshtml"
                                        Write(wedding.bride);

#line default
#line hidden
                EndContext();
                BeginContext(1046, 134, true);
                WriteLiteral("\r\n                            </a>\r\n                    </td>\r\n                    <td style=\"padding: 5px; border: 1px solid black;\">");
                EndContext();
                BeginContext(1181, 19, false);
#line 27 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\Home\Dashboard.cshtml"
                                                                  Write(wedding.WeddingDate);

#line default
#line hidden
                EndContext();
                BeginContext(1200, 78, true);
                WriteLiteral("</td>\r\n                    <td style=\"padding: 5px; border: 1px solid black;\">");
                EndContext();
                BeginContext(1279, 23, false);
#line 28 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\Home\Dashboard.cshtml"
                                                                  Write(wedding.Attending.Count);

#line default
#line hidden
                EndContext();
                BeginContext(1302, 7, true);
                WriteLiteral("</td>\r\n");
                EndContext();
#line 29 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\Home\Dashboard.cshtml"
                     if(@ViewBag.UserId == @wedding.UserId)
                    {

#line default
#line hidden
                BeginContext(1393, 77, true);
                WriteLiteral("                        <td style=\"padding: 5px; border: 1px solid black;\"><a");
                EndContext();
                BeginWriteAttribute("href", " href=\"", 1470, "\"", 1502, 2);
                WriteAttributeValue("", 1477, "delete/", 1477, 7, true);
#line 31 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\Home\Dashboard.cshtml"
WriteAttributeValue("", 1484, wedding.WeddingId, 1484, 18, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1503, 18, true);
                WriteLiteral(">Delete</a></td>\r\n");
                EndContext();
#line 32 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\Home\Dashboard.cshtml"
                    }
                    else
                    {
                            if(@wedding.Attending.Any(a => a.UserId == @ViewBag.UserId))
                            {

#line default
#line hidden
                BeginContext(1714, 85, true);
                WriteLiteral("                                <td style=\"padding: 5px; border: 1px solid black;\"><a");
                EndContext();
                BeginWriteAttribute("href", " href=\"", 1799, "\"", 1831, 2);
                WriteAttributeValue("", 1806, "unrsvp/", 1806, 7, true);
#line 37 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\Home\Dashboard.cshtml"
WriteAttributeValue("", 1813, wedding.WeddingId, 1813, 18, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1832, 19, true);
                WriteLiteral(">Un-RSVP</a></td>\r\n");
                EndContext();
#line 38 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\Home\Dashboard.cshtml"
                            }
                            else
                            {

#line default
#line hidden
                BeginContext(1947, 85, true);
                WriteLiteral("                                <td style=\"padding: 5px; border: 1px solid black;\"><a");
                EndContext();
                BeginWriteAttribute("href", " href=\"", 2032, "\"", 2062, 2);
                WriteAttributeValue("", 2039, "rsvp/", 2039, 5, true);
#line 41 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\Home\Dashboard.cshtml"
WriteAttributeValue("", 2044, wedding.WeddingId, 2044, 18, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2063, 16, true);
                WriteLiteral(">RSVP</a></td>\r\n");
                EndContext();
#line 42 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\Home\Dashboard.cshtml"
                            }
                    }

#line default
#line hidden
                BeginContext(2133, 23, true);
                WriteLiteral("                </tr>\r\n");
                EndContext();
#line 45 "C:\Users\ktari\Documents\Coding Dojo\c#\wedding_planner\Views\Home\Dashboard.cshtml"
            }

#line default
#line hidden
                BeginContext(2171, 14, true);
                WriteLiteral("    </table>\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2192, 9, true);
            WriteLiteral("\r\n</html>");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
