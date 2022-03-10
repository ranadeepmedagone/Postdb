using Postdb.Models;
using Dapper;
using Postdb.Utilities;
using Postdb.DTOs;

namespace Postdb.Repositories;


public interface IStudentRepository
{
    Task<Student> Create(Student Item);
    Task <bool> Update(Student item);
    Task <bool>Delete(long StudentId);
    Task<Student> GetById(long StudentId);
    Task<List<Student>> GetList();
    Task<List<StudentDTO>> GetList(long Id);

    

}
public class StudentRepository : BaseRepository, IStudentRepository
{
    public StudentRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Student> Create(Student item)
    {
        var query = $@"INSERT INTO ""{TableNames.student}""
        ( first_name, last_name, gender, date_of_birth, address, parent_mobile, email, join_date, class_id )
        VALUES ( @FirstName, @LastName, @Gender, @DateOfBirth, @Address, @ParentMobile, @Email, @JoinDate, @ClassId) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Student>(query, item);
            return res;
        }


    }

    public async Task<bool> Delete(long StudentId)
    {
        var query = $@"DELETE FROM ""{TableNames.student}""
        WHERE student_id = @StudentId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new {StudentId});
            return res > 0;
        } 
    }
     public async Task<Student> GetById(long StudentId)
    {
        var query = $@"SELECT * FROM ""{TableNames.student}""
        WHERE student_id = @studentid";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Student>(query,
            new
            {
            studentid = StudentId
            });

    }

    public async Task<List<Student>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.student}""";
        List<Student> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Student>(query)).AsList();
        return res;
    }

    public async Task<List<StudentDTO>> GetList(long Id)
    {
        var query = $@"SELECT * FROM {TableNames.student_teacher} st
        LEFT JOIN {TableNames.student} s ON s.student_id = st.student_id
        WHERE st.teacher_id = @Id";
 
        using (var con = NewConnection)
        {
            return(await con.QueryAsync<StudentDTO>(query, new{Id})).AsList();
        }
    }

    public async Task<bool> Update(Student item)
    {
        var query = $@"UPDATE ""{TableNames.student}"" SET last_name =@LastName,  
         mobile = @Mobile, email = @Email, address = @Address WHERE student_id = @StudentId";


        using  (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;

        }
    }
}
