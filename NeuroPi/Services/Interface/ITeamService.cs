using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.ViewModel.Team;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface ITeamService
    {
        ResponseResult<List<MTeamVM>> GetAllTeamsVM();
        ResponseResult<MTeamVM> GetTeamVMById(int id);
        //ResponseResult<object> CreateTeamVM(MTeamInsertVM vm);

        ResponseResult<MTeamVM> CreateTeamVM(MTeamInsertVM vm);

        ResponseResult<MTeamVM> UpdateTeamVM(int id, int tenantId, MTeamUpdateVM updatedTeam);
        ResponseResult<string> DeleteTeamVM(int id, int tenantId);
        ResponseResult<List<MTeamVM>> GetAllTeamsByTenantIdVM(int tenantId);
        ResponseResult<MTeamVM> GetByIdAndTenantIdVM(int id, int tenantId);
    }
}
