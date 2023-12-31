#pragma checksum "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ccfb61f185da029052412ace192fd98ab9f54dff"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_User__UserContent), @"mvc.1.0.view", @"/Areas/Admin/Views/User/_UserContent.cshtml")]
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
#line 1 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\_ViewImports.cshtml"
using BookStoreManagement.ClientApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\_ViewImports.cshtml"
using BookStoreManagement.ClientApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
using BookStoreManagement.ClientApp.Models.DTO;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ccfb61f185da029052412ace192fd98ab9f54dff", @"/Areas/Admin/Views/User/_UserContent.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27fe39ae35279f888f116e15e5ec9609301e3199", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_User__UserContent : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<UserDto>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "LockOrUnlock", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "User", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onsubmit", new global::Microsoft.AspNetCore.Html.HtmlString("return AjaxPost(this);"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"card-body table-responsive p-0\" id=\"author-table\">\r\n");
#nullable restore
#line 5 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
    if (Model.Count() > 0)
   {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"      <table class=""table table-bordered table-hover text-nowrap"">
         <thead class=""bg-dark"">
            <tr class=""text-white"">
               <th style=""text-align: center; vertical-align: middle"">Name</th>
               <th style=""text-align: center; vertical-align: middle"">Email</th>
               <th style=""text-align: center; vertical-align: middle"">Phone Number</th>
               <th style=""text-align: center; vertical-align: middle"">Role</th>
               <th style=""text-align: center; vertical-align: middle"">Status</th>
               <th style=""text-align: center; vertical-align: middle"">Action</th>
            </tr>
         </thead>
         <tbody>
");
#nullable restore
#line 19 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("               ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ccfb61f185da029052412ace192fd98ab9f54dff6309", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "id", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 21 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
AddHtmlAttributeValue("", 968, item.Id, 968, 8, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 22 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
                 WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 22 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
                                                  WriteLiteral(item.DisplayName);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["displayName"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-displayName", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["displayName"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 22 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
                                                                                         WriteLiteral(item.Status);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["isLocked"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-isLocked", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["isLocked"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n               <tr>\r\n                  <td style=\"text-align: center; vertical-align: middle\">");
#nullable restore
#line 24 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
                                                                    Write(item.DisplayName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                  <td style=\"text-align: center; vertical-align: middle\">");
#nullable restore
#line 25 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
                                                                    Write(item.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                  <td style=\"text-align: center; vertical-align: middle\">");
#nullable restore
#line 26 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
                                                                    Write(item.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                  <td style=\"text-align: center; vertical-align: middle\">");
#nullable restore
#line 27 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
                                                                    Write(item.Role);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                  <td class=\"project-state\" style=\"text-align: center; vertical-align: middle\">\r\n");
#nullable restore
#line 29 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
                      if (!item.Status)
                     {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <span class=\"badge badge-danger\">Locked</span>\r\n");
#nullable restore
#line 32 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
                     }
                     else
                     {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <span class=\"badge badge-success\">Active</span>\r\n");
#nullable restore
#line 36 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
                     }

#line default
#line hidden
#nullable disable
            WriteLiteral("                  </td>\r\n                  <td class=\"project-actions text-center\" style=\"width: 100px; text-align: center; vertical-align: middle\">\r\n                     <button class=\"btn btn-info btn-sm mr-1 ml-1\" title=\"Edit\" type=\"button\"");
            BeginWriteAttribute("onclick", "\r\n                       onclick=\"", 2230, "\"", 2371, 5);
            WriteAttributeValue("", 2264, "showCommonModal(\'", 2264, 17, true);
#nullable restore
#line 40 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
WriteAttributeValue("", 2281, Url.Action("Edit", "User", new { id = item.Id }, Context.Request.Scheme), 2281, 73, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2354, "\',", 2354, 2, true);
            WriteAttributeValue(" ", 2356, "\'Update", 2357, 8, true);
            WriteAttributeValue(" ", 2364, "User\')", 2365, 7, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <i class=\"fas fa-pencil-alt\"></i>\r\n                     </button>\r\n");
#nullable restore
#line 43 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
                      if (!item.Status)
                     {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <button class=\"btn btn-danger btn-sm mr-1 ml-1\" title=\"Unlock\" type=\"submit\"");
            BeginWriteAttribute("form", " form=\"", 2631, "\"", 2646, 1);
#nullable restore
#line 45 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
WriteAttributeValue("", 2638, item.Id, 2638, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                           <i class=\"fas fa-lock\"></i>\r\n                        </button>\r\n");
#nullable restore
#line 48 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
                     }
                     else
                     {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <button class=\"btn btn-success btn-sm mr-1 ml-1\" title=\"Lock\" type=\"submit\"");
            BeginWriteAttribute("form", " form=\"", 2915, "\"", 2930, 1);
#nullable restore
#line 51 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
WriteAttributeValue("", 2922, item.Id, 2922, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                           <i class=\"fas fa-lock-open\"></i>\r\n                        </button>\r\n");
#nullable restore
#line 54 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
                     }

#line default
#line hidden
#nullable disable
            WriteLiteral("                     <button class=\"btn btn-primary btn-sm mr-1 ml-1\" title=\"Change Password\" type=\"button\"");
            BeginWriteAttribute("onclick", "\r\n                       onclick=\"", 3161, "\"", 3316, 5);
            WriteAttributeValue("", 3195, "showCommonModal(\'", 3195, 17, true);
#nullable restore
#line 56 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
WriteAttributeValue("", 3212, Url.Action("ChangePassword", "User", new { id = item.Id }, Context.Request.Scheme), 3212, 83, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3295, "\',", 3295, 2, true);
            WriteAttributeValue(" ", 3297, "\'Change", 3298, 8, true);
            WriteAttributeValue(" ", 3305, "Password\')", 3306, 11, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <i class=\"fas fa-key\"></i>\r\n                     </button>\r\n                  </td>\r\n               </tr>\r\n");
#nullable restore
#line 61 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("         </tbody>\r\n      </table>\r\n");
#nullable restore
#line 64 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
   }
   else
   {

#line default
#line hidden
#nullable disable
            WriteLiteral("      <p>Have no any User...</p>\r\n");
#nullable restore
#line 68 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\User\_UserContent.cshtml"
   }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<UserDto>> Html { get; private set; }
    }
}
#pragma warning restore 1591
