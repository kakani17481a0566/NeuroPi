using NeuropiForms.ViewModels.Fields;

namespace NeuropiForms.Services.Interface
{
    public interface IFieldService
    {
        List<FieldsResponseVM> GetAllFields();
    
        FieldsResponseVM GetFieldById(int id);
    
        FieldsResponseVM GetFieldByIdAndTenantId(int id, int tenantId);
    
        FieldsResponseVM GetFieldByTenantId(int tenantId);
    
        FieldsResponseVM CreateField(FieldsRequestVM field);

        FieldsResponseVM UpdateField(int id, int tenantId, FieldsUpdateVM field);

        bool DeleteField(int id, int tenantId);
    }
}
