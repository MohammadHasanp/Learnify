using Learnify.Web.Infrastructure.RazorUtil;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Common.Application;
using Learnify.Web.ViewModels;
using User.Module.Data.Entities._Enum;
using UserModule.Core.Commands.Roles.Create;
using UserModule.Core.Commands.Roles.Delete;
using UserModule.Core.Commands.Roles.Edit;
using UserModule.Core.Queries.Roles.DTOs;
using UserModule.Core.Services;

namespace Learnify.Web.Areas.Admin.Pages.Roles
{
    public class IndexModel(IRoleService service, IRenderViewToString renderViewToString) : BaseRazorPage
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [BindProperty]
        public string Name { get; set; } = null!;
        public List<RoleDto> Roles { get; set; } = [];

        public async Task OnGet()
        {
            Roles = await service.GetAllRole();
        }

        public async Task<IActionResult> OnGetShowEditPage(Guid id)
        {
            return await AjaxTryCatch(async () =>
            {
                var role = await service.GetRoleById(id);
                if (role == null)
                {
                    return OperationResult<string>.NotFound();
                }
                var view = await renderViewToString.RenderToStringAsync("_Edit", new EditRoleViewModel
                {
                    Title = role.RoleTitle,
                    RoleId = id,
                    Permissions = role.Permissions,
                }, PageContext);
                return OperationResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnPostEdit(EditRoleViewModel viewModel, List<Permission> permissions)
        {
            return await AjaxTryCatch(() => service.Edit(new EditRoleCommand(viewModel.RoleId, permissions, viewModel.Title)));
        }
        public async Task<IActionResult> OnPost(List<Permission> permissions)
        {
            return await AjaxTryCatch(() => service.Create(new CreateRoleCommand(Name, permissions)));
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            return await AjaxTryCatch(() => service.Delete(new DeleteRoleCommand(id)));
        }
    }
}