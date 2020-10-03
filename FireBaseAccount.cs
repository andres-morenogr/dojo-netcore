using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dojo_netcore.Modelo;
using Google.Cloud.Firestore;

namespace dojo_netcore
{
  public class FireBaseAccount
  {
    private readonly static FireBaseAccount _instancia = new FireBaseAccount();
    FirestoreDb _db;
    public FireBaseAccount()
    {
      String path = AppDomain.CurrentDomain.BaseDirectory + @"Firebase-SDK.json";
      Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
      _db = FirestoreDb.Create("dojo-net-core");
      Console.WriteLine("Mela la conexión");
    }

    public static FireBaseAccount Instancia
    {
      get
      {
        return _instancia;
      }
    }

    public async Task<String> AddUser(Usuario usuario)
    {
      DocumentReference coll = _db.Collection("Usuarios").Document();
      Dictionary<String, Object> data = new Dictionary<String, Object>()
            {
                {"Nombre", usuario.Nombre},
                {"Cedula", usuario.Cedula},
                {"Correo", usuario.Correo},
                {"Carrera", usuario.Carrera}
            };
      await coll.SetAsync(data);

      return "Usuario guardado con Id: " + coll.Id;
    }

    public async Task<List<Usuario>> GetUser()
    {
      CollectionReference usuariosRef = _db.Collection("Usuarios");
      Query query = usuariosRef;
      QuerySnapshot queryUser = await usuariosRef.GetSnapshotAsync();
      List<Usuario> ListaUsuarios = new List<Usuario>();
      foreach (DocumentSnapshot documentSnapshot in queryUser.Documents)
      {
        Usuario usuario = new Usuario();
        Dictionary<String, Object> usuarios = documentSnapshot.ToDictionary();
        foreach (var item in usuarios)
        {
          if (item.Key == "Nombre")
          {
            usuario.Nombre = (String)item.Value;
          }
          else if (item.Key == "Cedula")
          {
            usuario.Cedula = (String)item.Value;
          }
          else if (item.Key == "Correo")
          {
            usuario.Correo = (String)item.Value;
          }
          else if (item.Key == "Carrera")
          {
            usuario.Carrera = (String)item.Value;
          }
        }
        ListaUsuarios.Add(usuario);
      }
      return ListaUsuarios;
    }

    public async Task<String> DeleteUser(String id)
    {
      DocumentReference cityRef = _db.Collection("Usuarios").Document(id);
      await cityRef.DeleteAsync();
      return "Usario con Id: " + id + " eliminado correctamente";
    }
  }
}