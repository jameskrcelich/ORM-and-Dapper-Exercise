using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace ORM_Dapper;

public class DapperDepartmentRepository : IDepartmentRepository
{
    private readonly IDbConnection _connection;

    public DapperDepartmentRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public IEnumerable<Department> GetDepartments()
    {
        return _connection.Query<Department>("SELECT * FROM departments;");
    }
    
    public void AddDepartment( string newDepartmentName )
    {
        _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);",
            new { departmentName = newDepartmentName });
    }
    
    public void DeleteDepartment( int deptID )
    {
        _connection.Execute("DELETE from departments WHERE DepartmentID = @deptID;",
            new { deptID = deptID });
    }
}