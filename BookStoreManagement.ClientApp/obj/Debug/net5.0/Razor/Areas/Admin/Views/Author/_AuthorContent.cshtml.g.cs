#pragma checksum "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\Author\_AuthorContent.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "34091536c2bca5359eea777d7f2c0ea9b4d08984"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Author__AuthorContent), @"mvc.1.0.view", @"/Areas/Admin/Views/Author/_AuthorContent.cshtml")]
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
#line 1 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\Author\_AuthorContent.cshtml"
using BookStoreManagement.ClientApp.Models.DTO;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"34091536c2bca5359eea777d7f2c0ea9b4d08984", @"/Areas/Admin/Views/Author/_AuthorContent.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27fe39ae35279f888f116e15e5ec9609301e3199", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Author__AuthorContent : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<AuthorDto>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"card-body table-responsive p-0\" id=\"author-table\">\r\n");
#nullable restore
#line 5 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\Author\_AuthorContent.cshtml"
    if (Model.Count() > 0)
   {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"      <table class=""table table-bordered table-hover text-nowrap"">
         <thead class=""bg-dark"">
            <tr class=""text-white"">
               <th style=""width: 20px; text-align: center; vertical-align: middle"">#</th>
               <th style=""min-width: 400px"">Author Name</th>
               <th style=""width: 150px; text-align: center; vertical-align: middle"">Book Count</th>
               <th style=""width: 250px; text-align: center; vertical-align: middle"">Action</th>
            </tr>
         </thead>
         <tbody>
");
#nullable restore
#line 17 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\Author\_AuthorContent.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("               <tr>\r\n                  <td style=\"text-align: center; vertical-align: middle\">");
#nullable restore
#line 20 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\Author\_AuthorContent.cshtml"
                                                                    Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                  <td>");
#nullable restore
#line 21 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\Author\_AuthorContent.cshtml"
                 Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                  <td style=\"text-align: center; vertical-align: middle\">");
#nullable restore
#line 22 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\Author\_AuthorContent.cshtml"
                                                                    Write(item.BookCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                  <td class=\"project-actions text-center\" style=\"width: 100px; text-align: center; vertical-align: middle\">\r\n                     <button class=\"btn btn-primary btn-sm mr-1 ml-1\" title=\"Detail\" type=\"button\"");
            BeginWriteAttribute("onclick", "\r\n                       onclick=\"", 1249, "\"", 1396, 5);
            WriteAttributeValue("", 1283, "showCommonModal(\'", 1283, 17, true);
#nullable restore
#line 25 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\Author\_AuthorContent.cshtml"
WriteAttributeValue("", 1300, Url.Action("Detail", "Author", new { id = item.Id }, Context.Request.Scheme), 1300, 77, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1377, "\',", 1377, 2, true);
            WriteAttributeValue(" ", 1379, "\'Author", 1380, 8, true);
            WriteAttributeValue(" ", 1387, "Detail\')", 1388, 9, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <i class=\"fas fa-eye\"></i>\r\n                     </button>\r\n                     <button class=\"btn btn-info btn-sm mr-1 ml-1\" title=\"Edit\" type=\"button\"");
            BeginWriteAttribute("onclick", "\r\n                       onclick=\"", 1577, "\"", 1727, 5);
            WriteAttributeValue("", 1611, "showCommonModal(\'", 1611, 17, true);
#nullable restore
#line 29 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\Author\_AuthorContent.cshtml"
WriteAttributeValue("", 1628, Url.Action("AddOrEdit", "Author", new { id = item.Id }, Context.Request.Scheme), 1628, 80, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1708, "\',", 1708, 2, true);
            WriteAttributeValue(" ", 1710, "\'Update", 1711, 8, true);
            WriteAttributeValue(" ", 1718, "Author\')", 1719, 9, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <i class=\"fas fa-pencil-alt\"></i>\r\n                     </button>\r\n                     <button class=\"btn btn-danger btn-sm mr-1 ml-1\" title=\"Delete\" type=\"button\"");
            BeginWriteAttribute("onclick", "\r\n                       onclick=\"", 1919, "\"", 2066, 5);
            WriteAttributeValue("", 1953, "showDeleteModal(\'", 1953, 17, true);
#nullable restore
#line 33 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\Author\_AuthorContent.cshtml"
WriteAttributeValue("", 1970, Url.Action("Delete", "Author", new { id = item.Id }, Context.Request.Scheme), 1970, 77, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2047, "\',", 2047, 2, true);
            WriteAttributeValue(" ", 2049, "\'Delete", 2050, 8, true);
            WriteAttributeValue(" ", 2057, "Author\')", 2058, 9, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <i class=\"fas fa-trash\"></i>\r\n                     </button>\r\n                  </td>\r\n               </tr>\r\n");
#nullable restore
#line 38 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\Author\_AuthorContent.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("         </tbody>\r\n      </table>\r\n");
#nullable restore
#line 41 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\Author\_AuthorContent.cshtml"
   }
   else
   {

#line default
#line hidden
#nullable disable
            WriteLiteral("      <p>Have no any author was added...</p>\r\n");
#nullable restore
#line 45 "D:\.NET Core\ProjectPV\BookStoreManagement\BookStoreManagement.ClientApp\Areas\Admin\Views\Author\_AuthorContent.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<AuthorDto>> Html { get; private set; }
    }
}
#pragma warning restore 1591
