using Api.DTOs;
using Dominio.Entities;

namespace Api.Mappings;

public static class MiembroMapping
{
    public static MiembroDto ToDto(Miembro miembro)
    {
        if (miembro == null) return null!;

        return new MiembroDto
        {
            Id = miembro.Id,
            Nombre = miembro.Nombre,
            Apellido = miembro.Apellido,
            Celular = miembro.Celular,
            CorreoElectronico = miembro.CorreoElectronico,
            FechaIngreso = miembro.FechaIngreso,
            Direccion = miembro.Direccion,
            MemberNumber = miembro.MemberNumber,
            Cargo = miembro.Cargo,
            Rango = miembro.Rango,
            Estatus = miembro.Estatus,
            FechaNacimiento = miembro.FechaNacimiento,
            Cedula = miembro.Cedula,
            RH = miembro.RH,
            EPS = miembro.EPS,
            Padrino = miembro.Padrino,
            Foto = miembro.Foto,
            ContactoEmergencia = miembro.ContactoEmergencia,
            Ciudad = miembro.Ciudad,
            Moto = miembro.Moto,
            AnoModelo = miembro.AnoModelo,
            Marca = miembro.Marca,
            CilindrajeCC = miembro.CilindrajeCC,
            PlacaMatricula = miembro.PlacaMatricula,
            FechaExpedicionLicenciaConduccion = miembro.FechaExpedicionLicenciaConduccion,
            FechaExpedicionSOAT = miembro.FechaExpedicionSOAT
        };
    }

    public static Miembro ToDomain(CreateMiembroDto dto)
    {
        if (dto == null) return null!;

        return new Miembro
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Celular = dto.Celular,
            CorreoElectronico = dto.CorreoElectronico,
            FechaIngreso = dto.FechaIngreso,
            Direccion = dto.Direccion,
            MemberNumber = dto.MemberNumber,
            Cargo = dto.Cargo,
            Rango = dto.Rango,
            Estatus = dto.Estatus,
            FechaNacimiento = dto.FechaNacimiento,
            Cedula = dto.Cedula,
            RH = dto.RH,
            EPS = dto.EPS,
            Padrino = dto.Padrino,
            Foto = dto.Foto,
            ContactoEmergencia = dto.ContactoEmergencia,
            Ciudad = dto.Ciudad,
            Moto = dto.Moto,
            AnoModelo = dto.AnoModelo,
            Marca = dto.Marca,
            CilindrajeCC = dto.CilindrajeCC,
            PlacaMatricula = dto.PlacaMatricula,
            FechaExpedicionLicenciaConduccion = dto.FechaExpedicionLicenciaConduccion,
            FechaExpedicionSOAT = dto.FechaExpedicionSOAT
        };
    }

    public static Miembro ToDomain(UpdateMiembroDto dto, int id)
    {
        if (dto == null) return null!;

        return new Miembro
        {
            Id = id,
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Celular = dto.Celular,
            CorreoElectronico = dto.CorreoElectronico,
            FechaIngreso = dto.FechaIngreso,
            Direccion = dto.Direccion,
            MemberNumber = dto.MemberNumber,
            Cargo = dto.Cargo,
            Rango = dto.Rango,
            Estatus = dto.Estatus,
            FechaNacimiento = dto.FechaNacimiento,
            Cedula = dto.Cedula,
            RH = dto.RH,
            EPS = dto.EPS,
            Padrino = dto.Padrino,
            Foto = dto.Foto,
            ContactoEmergencia = dto.ContactoEmergencia,
            Ciudad = dto.Ciudad,
            Moto = dto.Moto,
            AnoModelo = dto.AnoModelo,
            Marca = dto.Marca,
            CilindrajeCC = dto.CilindrajeCC,
            PlacaMatricula = dto.PlacaMatricula,
            FechaExpedicionLicenciaConduccion = dto.FechaExpedicionLicenciaConduccion,
            FechaExpedicionSOAT = dto.FechaExpedicionSOAT
        };
    }
}

