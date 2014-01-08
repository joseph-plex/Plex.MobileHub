using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data
{
    public interface IPlexxisDatabaseRow
    {

        //for one shot changes (immediate commit)
        bool Insert();
        bool Update();
        bool Delete();

        //Makes it possible to do transactions
        bool Insert(OracleConnection Conn);
        bool Update(OracleConnection Conn);
        bool Delete(OracleConnection Conn);
    }
}