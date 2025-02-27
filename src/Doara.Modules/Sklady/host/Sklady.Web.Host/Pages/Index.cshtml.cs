using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Sklady.Pages;

public class IndexModel : SkladyPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
