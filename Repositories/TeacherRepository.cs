using Postdb.Models;
using Dapper;
using Postdb.Utilities;

namespace Postdb.Repositories;


public interface ITeacherRepository
{
    Task<Teacher> Create(Teacher Item);
    Task<bool> Update(Teacher item);
    Task<bool> Delete(long TeacherId);
    Task<Teacher> GetById(long TeacherId);
    Task<List<Teacher>> GetList();
    Task<List<TeacherDTO>> GetList(long Id);
    Task<List<TeacherDTO>> GetListBySubject(long SubjectId);
}
public class TeacherRepository : BaseRepository, ITeacherRepository
{
    public TeacherRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Teacher> Create(Teacher item)
    {
        var query = $@"INSERT INTO ""{TableNames.teacher}""
        ( first_name, last_name, gender, birth_day, mobile, address  ,date_of_join, email, subject )
        VALUES ( @FirstName, @LastName, @Gender, @BirthDay, @Mobile, @Address, @DateOfJoin, @Email, @Subject) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Teacher>(query, item);
            return res;
        }


    }

    public async Task<bool> Delete(long TeacherId)
    {
        var query = $@"DELETE FROM ""{TableNames.teacher}""
        WHERE Teacher_id = @TeacherId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { TeacherId });
            return res > 0;
        }
    }
    public async Task<Teacher> GetById(long TeacherId)
    {
        var query = $@"SELECT * FROM ""{TableNames.teacher}""
        WHERE Teacher_id = @TeacherId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Teacher>(query,
            new
            {
                TeacherId = TeacherId
            });

    }

    public async Task<List<Teacher>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.teacher}""";
        List<Teacher> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Teacher>(query)).AsList();
        return res;
    }

    public async Task<List<TeacherDTO>> GetList(long Id)
    {
        var query = $@"SELECT t.* FROM {TableNames.student_teacher} st
     LEFT JOIN {TableNames.teacher} t ON t.teacher_id = st.teacher_id
     WHERE st.student_id = @Id";

        using (var con = NewConnection)
        {
            return (await con.QueryAsync<TeacherDTO>(query, new { Id })).AsList();
        }
    }
    

    public async Task<List<TeacherDTO>> GetListBySubject(long SubjectId)
    {
        var query = $@"SELECT t.* FROM {TableNames.teacher} t
     WHERE t.sub_id = @SubjectId";

        using (var con = NewConnection)
        {
            return (await con.QueryAsync<TeacherDTO>(query, new { SubjectId })).AsList();
        }
    }

    public async Task<bool> Update(Teacher item)
    {
        var query = $@"UPDATE ""{TableNames.teacher}"" SET last_name = @LastName, 
         mobile = @Mobile, email = @Email, address = @Address, subject = @Subject WHERE Teacher_id = @TeacherId";


        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;

        }
    }
}
