#pragma checksum "C:\Users\可爱的我\Documents\小可爱的IP作业\Internet-Programming\Lab\Lab-10\Lab-10\Views\Home\StoryDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "947b322e56146426990dd32c86f44f4b1c559859"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_StoryDetails), @"mvc.1.0.view", @"/Views/Home/StoryDetails.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/StoryDetails.cshtml", typeof(AspNetCore.Views_Home_StoryDetails))]
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
#line 1 "C:\Users\可爱的我\Documents\小可爱的IP作业\Internet-Programming\Lab\Lab-10\Lab-10\Views\_ViewImports.cshtml"
using Lab10;

#line default
#line hidden
#line 2 "C:\Users\可爱的我\Documents\小可爱的IP作业\Internet-Programming\Lab\Lab-10\Lab-10\Views\_ViewImports.cshtml"
using Lab10.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"947b322e56146426990dd32c86f44f4b1c559859", @"/Views/Home/StoryDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"645b75b26b1403c3a348454464574bfd1ee5e623", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_StoryDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Lab10.Models.Story>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(27, 139, true);
            WriteLiteral("<!--\r\n  CourseDetails.cshtml - show course details including lectures\r\n\r\n  Jim Fawcett, CSE686 - Internet Programming, Spring 2019\r\n-->\r\n\r\n");
            EndContext();
#line 8 "C:\Users\可爱的我\Documents\小可爱的IP作业\Internet-Programming\Lab\Lab-10\Lab-10\Views\Home\StoryDetails.cshtml"
  
  ViewData["Title"] = "StoryDetails";
  Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(257, 97, true);
            WriteLiteral("\r\n<h1>StoryDetails</h1>\r\n\r\n<div>\r\n  <hr />\r\n  <dl class=\"row\">\r\n    <dt class=\"col-sm-2\">\r\n      ");
            EndContext();
            BeginContext(355, 41, false);
#line 19 "C:\Users\可爱的我\Documents\小可爱的IP作业\Internet-Programming\Lab\Lab-10\Lab-10\Views\Home\StoryDetails.cshtml"
 Write(Html.DisplayNameFor(model => model.Title));

#line default
#line hidden
            EndContext();
            BeginContext(396, 47, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n      ");
            EndContext();
            BeginContext(444, 37, false);
#line 22 "C:\Users\可爱的我\Documents\小可爱的IP作业\Internet-Programming\Lab\Lab-10\Lab-10\Views\Home\StoryDetails.cshtml"
 Write(Html.DisplayFor(model => model.Title));

#line default
#line hidden
            EndContext();
            BeginContext(481, 46, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n      ");
            EndContext();
            BeginContext(528, 47, false);
#line 25 "C:\Users\可爱的我\Documents\小可爱的IP作业\Internet-Programming\Lab\Lab-10\Lab-10\Views\Home\StoryDetails.cshtml"
 Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
            EndContext();
            BeginContext(575, 47, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n      ");
            EndContext();
            BeginContext(623, 43, false);
#line 28 "C:\Users\可爱的我\Documents\小可爱的IP作业\Internet-Programming\Lab\Lab-10\Lab-10\Views\Home\StoryDetails.cshtml"
 Write(Html.DisplayFor(model => model.Description));

#line default
#line hidden
            EndContext();
            BeginContext(666, 37, true);
            WriteLiteral("\r\n    </dd>\r\n  </dl>\r\n</div>\r\n<div>\r\n");
            EndContext();
#line 33 "C:\Users\可爱的我\Documents\小可爱的IP作业\Internet-Programming\Lab\Lab-10\Lab-10\Views\Home\StoryDetails.cshtml"
   foreach (var lct in Model.Comments)
  {

#line default
#line hidden
            BeginContext(748, 55, true);
            WriteLiteral("    <div style=\"padding:0px 25px; margin:0px;\">\r\n      ");
            EndContext();
            BeginContext(804, 24, false);
#line 36 "C:\Users\可爱的我\Documents\小可爱的IP作业\Internet-Programming\Lab\Lab-10\Lab-10\Views\Home\StoryDetails.cshtml"
 Write(Html.Label(lct.Username));

#line default
#line hidden
            EndContext();
            BeginContext(828, 10, true);
            WriteLiteral(" -\r\n      ");
            EndContext();
            BeginContext(839, 23, false);
#line 37 "C:\Users\可爱的我\Documents\小可爱的IP作业\Internet-Programming\Lab\Lab-10\Lab-10\Views\Home\StoryDetails.cshtml"
 Write(Html.Label(lct.Content));

#line default
#line hidden
            EndContext();
            BeginContext(862, 14, true);
            WriteLiteral("\r\n    </div>\r\n");
            EndContext();
#line 39 "C:\Users\可爱的我\Documents\小可爱的IP作业\Internet-Programming\Lab\Lab-10\Lab-10\Views\Home\StoryDetails.cshtml"
  }

#line default
#line hidden
            BeginContext(881, 52, true);
            WriteLiteral("</div>\r\n<div style=\"height: 10px;\"></div>\r\n<div>\r\n  ");
            EndContext();
            BeginContext(933, 59, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "947b322e56146426990dd32c86f44f4b1c5598597546", async() => {
                BeginContext(984, 4, true);
                WriteLiteral("Edit");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 43 "C:\Users\可爱的我\Documents\小可爱的IP作业\Internet-Programming\Lab\Lab-10\Lab-10\Views\Home\StoryDetails.cshtml"
                         WriteLiteral(Model.StoryID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(992, 6, true);
            WriteLiteral(" |\r\n  ");
            EndContext();
            BeginContext(998, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "947b322e56146426990dd32c86f44f4b1c5598599888", async() => {
                BeginContext(1020, 12, true);
                WriteLiteral("Back to List");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1036, 10, true);
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Lab10.Models.Story> Html { get; private set; }
    }
}
#pragma warning restore 1591
