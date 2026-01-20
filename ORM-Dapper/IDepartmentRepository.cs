namespace ORM_Dapper;

public interface IDepartmentRepository
{
    IEnumerable<Department> GetDepartments();
    void AddDepartment( string department );
    void DeleteDepartment( int departmentID );
}