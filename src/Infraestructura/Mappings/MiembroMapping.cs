using Dominio.Entities;
using Infraestructura.DataEntities;

namespace Infraestructura.Mappings;

public static class MiembroMapping
{
    public static Miembro ToDomain(DataModelMiembro dataModel)
    {
        if (dataModel == null) return null!;

        return new Miembro
        {
            Id = dataModel.Id,
            Nombre = dataModel.Nombre,
            Apellido = dataModel.Apellido,
            Celular = dataModel.Celular,
            CorreoElectronico = dataModel.CorreoElectronico,
            FechaIngreso = dataModel.FechaIngreso,
            Direccion = dataModel.Direccion,
            MemberNumber = dataModel.MemberNumber,
            Cargo = dataModel.Cargo,
            Rango = dataModel.Rango,
            Estatus = dataModel.Estatus,
            FechaNacimiento = dataModel.FechaNacimiento,
            Cedula = dataModel.Cedula,
            RH = dataModel.RH,
            EPS = dataModel.EPS,
            Padrino = dataModel.Padrino,
            Foto = dataModel.Foto,
            ContactoEmergencia = dataModel.ContactoEmergencia,
            Ciudad = dataModel.Ciudad,
            Moto = dataModel.Moto,
            AnoModelo = dataModel.AnoModelo,
            Marca = dataModel.Marca,
            CilindrajeCC = dataModel.CilindrajeCC,
            PlacaMatricula = dataModel.PlacaMatricula,
            FechaExpedicionLicenciaConduccion = dataModel.FechaExpedicionLicenciaConduccion,
            FechaExpedicionSOAT = dataModel.FechaExpedicionSOAT
        };
    }

    public static DataModelMiembro ToDataModel(Miembro miembro)
    {
        if (miembro == null) return null!;

        return new DataModelMiembro
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
}

