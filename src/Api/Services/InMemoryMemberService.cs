using Domain.Entities;

namespace Api.Services;

public class InMemoryMemberService : IMemberService
{
    private readonly List<Member> _members = [];
    private int _nextId = 1;
    private readonly object _lock = new();

    public IEnumerable<Member> GetAll()
    {
        lock (_lock)
        {
            return _members.Select(Clone).ToList();
        }
    }

    public Member? GetById(int id)
    {
        lock (_lock)
        {
            var member = _members.FirstOrDefault(m => m.Id == id);
            return member is null ? null : Clone(member);
        }
    }

    public Member Create(Member member)
    {
        lock (_lock)
        {
            member.Id = _nextId++;
            var stored = Clone(member);
            _members.Add(stored);
            return Clone(stored);
        }
    }

    public bool Update(int id, Member member)
    {
        lock (_lock)
        {
            var index = _members.FindIndex(m => m.Id == id);
            if (index == -1)
            {
                return false;
            }

            member.Id = id;
            _members[index] = Clone(member);
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            var member = _members.FirstOrDefault(m => m.Id == id);
            if (member is null)
            {
                return false;
            }

            _members.Remove(member);
            return true;
        }
    }

    private static Member Clone(Member member) => new()
    {
        Id = member.Id,
        Nombre = member.Nombre,
        Apellido = member.Apellido,
        Celular = member.Celular,
        CorreoElectronico = member.CorreoElectronico,
        FechaIngreso = member.FechaIngreso,
        Direccion = member.Direccion,
        MemberNumber = member.MemberNumber,
        Cargo = member.Cargo,
        Rango = member.Rango,
        Estatus = member.Estatus,
        FechaNacimiento = member.FechaNacimiento,
        Cedula = member.Cedula,
        RH = member.RH,
        EPS = member.EPS,
        Padrino = member.Padrino,
        Foto = member.Foto,
        ContactoEmergencia = member.ContactoEmergencia,
        Ciudad = member.Ciudad,
        Moto = member.Moto,
        AnoModelo = member.AnoModelo,
        Marca = member.Marca,
        CilindrajeCC = member.CilindrajeCC,
        PlacaMatricula = member.PlacaMatricula,
        FechaExpedicionLicenciaConduccion = member.FechaExpedicionLicenciaConduccion,
        FechaExpedicionSOAT = member.FechaExpedicionSOAT
    };
}
