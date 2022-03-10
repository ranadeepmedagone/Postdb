using Postdb.Models;
using Dapper;
using Postdb.Utilities;
using Postdb.DTOs;

namespace Postdb.Repositories;


public interface ISubjectsRepository
{
    // Task<Subjects> Create(Subjects Item);
    // Task <bool> Update(Subjects item);
    // Task <bool>Delete(long SubjectsId);
    Task<Subjects> GetById(long SubjectsId);
    Task<List<Subjects>> GetList();
    Task<List<SubjectsDTO>> GetSubjects(long Id);

    

}
public class SubjectsRepository : BaseRepository, ISubjectsRepository
{
    public SubjectsRepository(IConfiguration config) : base(config)
    {

    }

    // public Task<Subjects> Create(Subjects Item)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<bool> Delete(long SubjectsId)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<Subjects> GetById(long SubjectsId)
    {
        var query = $@"SELECT * FROM ""{TableNames.subjects}""
        WHERE Sub_id = @Subid";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Subjects>(query,
            new
            {
            SubId = SubjectsId
            });

    }

    public async Task<List<Subjects>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.subjects}""";
        List<Subjects> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Subjects>(query)).AsList();
        return res;
    }

    public async Task<List<SubjectsDTO>> GetSubjects(long Id)
    {
        var query = $@"SELECT * FROM {TableNames.student_subjects} ss
        LEFT JOIN {TableNames.subjects} s ON s.sub _id = ss.sub_id
        WHERE ss.student_id = @Id";

        using(var con = NewConnection)
        {
            return(await con.QueryAsync<SubjectsDTO>(query, new{Id})).AsList();
        }
    }

    // public async Task<List<SubjectsDTO>> GetList(long Id)
    // {
    //     var query = $@"SELECT * FROM {TableNames.subjects_student} st
    //     LEFT JOIN {TableNames.subjects} s ON s.Subjects_id = st.Subjects_id
    //     WHERE st.student_id = @Id";

    //     using (var con = NewConnection)
    //     {
    //         return(await con.QueryAsync<SubjectsDTO>(query, new{Id})).AsList();
    //     }
    // }

    // public Task<bool> Update(Subjects item)
    // {
    //     throw new NotImplementedException();
    // }

    // public async Task<bool> Update(Subjects item)
    // {
    //     var query = $@"UPDATE ""{TableNames.subjects}"" SET last_name =@LastName,  
    //      mobile = @Mobile, email = @Email, address = @Address WHERE Subjects_id = @SubjectsId";


    //     using  (var con = NewConnection)
    //     {
    //         var rowCount = await con.ExecuteAsync(query, item);
    //         return rowCount == 1;

    //     }
    // }
}
