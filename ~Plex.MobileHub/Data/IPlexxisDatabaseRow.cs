using System;
using System.Data;

namespace Plex.MobileHub.Data
{
    public interface IPlexxisDatabaseRow
    {
        //for one shot changes (immediate commit)
        bool Insert();
        bool Update();
        bool Delete();

        //Makes it possible to do transactions
        bool Insert(IDbConnection Conn);
        bool Update(IDbConnection Conn);
        bool Delete(IDbConnection Conn);
    }
}