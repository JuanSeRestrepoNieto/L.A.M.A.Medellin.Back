using Domain.Entities;

namespace Api.Services;

public interface IMemberService
{
    IEnumerable<Member> GetAll();
    Member? GetById(int id);
    Member Create(Member member);
    bool Update(int id, Member member);
    bool Delete(int id);
}
