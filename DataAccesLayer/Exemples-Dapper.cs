using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;

//--------------------------------------------------------------------------------------------------------------//
//-- class for example                                                                                        --//
//--------------------------------------------------------------------------------------------------------------//

namespace DatasAccesLayer
{
    //http://dapper-tutorial.net/

    public abstract class Exemples_Dapper
    {
        /*
        Dapper étendra votre interface IDbConnection avec plusieurs méthodes:

        Execute
        Query
        QueryFirst
        QueryFirstOrDefault
        QuerySingle
        QuerySingleOrDefault
        QueryMultiple
        */
        private void Exemple001()
        {
            /*
            using (var connection = ConnectionFactory())
            {
                string sqlInvoices = "SELECT * FROM Invoice;";
                var invoices = connection.Query<Invoice>(sqlInvoices).ToList();

                string sqlInvoice = "SELECT * FROM Invoice WHERE InvoiceID = @InvoiceID;";
                var invoice = connection.QueryFirstOrDefault(sqlInvoice, new { InvoiceID = 1 });

                string sp = "EXEC Invoice_Insert";
                var affectedRows = connection.Execute(sp, new { Param1 = "Single_Insert_1" }, commandType: CommandType.StoredProcedure);
            }
            */
        }

        /*
        Paramètre

        La méthode Execute and Queries peut utiliser des paramètres de plusieurs manières différentes:

        Anonyme
        Dynamique
        liste
        Chaîne
        */
        private void Exemple002()
        {
            /*
            using (var connection = ConnectionFactory())
            {
                // Anonymous 
                var affectedRows = connection.Execute(sql, new { Kind = InvoiceKind.WebInvoice, Code = "Single_Insert_1" }, commandType: CommandType.StoredProcedure);

                // Dynamic 
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@Kind", InvoiceKind.WebInvoice, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Code", "Many_Insert_0", DbType.String, ParameterDirection.Input);
                parameter.Add("@RowCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                connection.Execute(sql, new { Kind = InvoiceKind.WebInvoice, Code = "Single_Insert_1" }, commandType: CommandType.StoredProcedure);

                // List 
                connection.Query<Invoice>(sql, new { Kind = new[] { InvoiceKind.StoreInvoice, InvoiceKind.WebInvoice } }).ToList();

                // String 
                connection.Query<Invoice>(sql, new { Code = new DbString { Value = "Invoice_1", IsFixedLength = false, Length = 9, IsAnsi = true } }).ToList();
            }
            */
        }

        /*
        Résultat

        Le résultat renvoyé par la méthode query peut être mappé à plusieurs types:

        Anonyme
        Fortement typé
        Multi-mapping
        Multi-résultats
        Multi-Type
        */

        private void Exemple003()
        {
            /*
            using (var connection = ConnectionFactory())
            {
                string sql = "SELECT * FROM Invoice;";
                
                connection.Open();

                var anonymousList = connection.Query(sql).ToList();

                var invoices = connection.Query<Invoice>(sql).ToList();                
            }
            */
        }

        /*
        Utilitaires

        Async
        Tampon
        Transaction
        Procédure stockée
        */
        private void Exemple004()
        {
            /*
            using (var connection = ConnectionFactory())
            {
                //Async 
                connection.QueryAsync<Invoice>(sql);

                //Buffered 
                connection.Query<Invoice>(sql, buffered : false )

                //Transaction                 
                using (var transaction = connection.BeginTransaction())
                {
                    var affectedRows = connection.Execute(sql, new { Kind = InvoiceKind.WebInvoice, Code = "Single_Insert_1" }, commandType: CommandType.StoredProcedure, transaction: transaction);

                    transaction.Commit();
                }

                //Stored Procedure 
                var affectedRows = connection.Execute(sql, new { Kind = InvoiceKind.WebInvoice, Code = "Single_Insert_1" }, commandType : CommandType.StoredProcedure ); 
            }
            */
        }

        /*
        Execute est une méthode d'extension qui peut être appelée depuis n'importe quel objet de type IDbConnection. 
        Il peut exécuter une commande une ou plusieurs fois et renvoyer le nombre de lignes affectées. 
        Cette méthode est généralement utilisée pour exécuter:

        Procédure stockée
        Instruction INSERT
        Déclaration UPDATE
        Instruction DELETE 

        Paramètres

        Le tableau suivant montre le paramètre différent d'une méthode Execute.
        nom 	        description
        sql 	        Le texte de la commande à exécuter.
        param 	        Les paramètres de la commande (default = null).
        transaction 	La transaction à utiliser (default = null).
        commandTimeout 	La commande timeout (default = null)
        commandType 	Le type de commande (default = null)

        Unique

        Exécuter la procédure stockée une seule fois. 
        */

        private void Exemple005()
        {
            /*
            using (var connection = ConnectionFactory())
            {
                string sql = "EXEC Invoice_Insert";

                connection.Open();

                var affectedRows = connection.Execute(sql, new { Kind = InvoiceKind.WebInvoice, Code = "Single_Insert_1" }, commandType: CommandType.StoredProcedure);

                My.Result.Show(affectedRows);
            }
            */
        }

        /*
        Beaucoup

        Exécuter la procédure stockée plusieurs fois. Une fois pour chaque objet dans la liste du tableau.  
        */
        private void Exemple006()
        {
            /*
            string sql = "EXEC Invoice_Insert";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var affectedRows = connection.Execute(sql,
                    new[]
                    {
                        new {Kind = InvoiceKind.WebInvoice, Code = "Many_Insert_1"},
                        new {Kind = InvoiceKind.WebInvoice, Code = "Many_Insert_2"},
                        new {Kind = InvoiceKind.StoreInvoice, Code = "Many_Insert_3"}
                    },
                    commandType: CommandType.StoredProcedure
                );

                My.Result.Show(affectedRows);
            }
            */
        }

        /*
        Exemple - Exécuter INSERT

        Unique

        Exécutez l'instruction INSERT une seule fois.
        */
        private void Exemple007()
        {
            /*
            string sql = "INSERT INTO Invoice (Code) Values (@Code);";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var affectedRows = connection.Execute(sql, new { Kind = InvoiceKind.WebInvoice, Code = "Single_Insert_1" });

                My.Result.Show(affectedRows);
            }
            */
        }

        /*
        Beaucoup

        Exécutez l'instruction INSERT plusieurs fois. Une fois pour chaque objet dans la liste du tableau.
        */
        private void Exemple008()
        {
            /*
            string sql = "INSERT INTO Invoice (Code) Values (@Code);";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var affectedRows = connection.Execute(sql,
                    new[]
                    {
                        new {Kind = InvoiceKind.WebInvoice, Code = "Many_Insert_1"},
                        new {Kind = InvoiceKind.WebInvoice, Code = "Many_Insert_2"},
                        new {Kind = InvoiceKind.StoreInvoice, Code = "Many_Insert_3"}
                    }
                );

                My.Result.Show(affectedRows);
            }
            */
        }

        /*
        Exemple - Exécuter UPDATE

        Unique

        Exécutez l'instruction UPDATE une seule fois.
        */

        private void Exemple009()
        {
            /*
            string sql = "UPDATE Invoice SET Code = @Code WHERE InvoiceID = @InvoiceID";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var affectedRows = connection.Execute(sql, new { InvoiceID = 1, Code = "Single_Update_1" });

                My.Result.Show(affectedRows);
            }
            */
        }
        /*
        Beaucoup

        Exécutez l'instruction UPDATE plusieurs fois. Une fois pour chaque objet dans la liste du tableau.
        */
        private void Exemple010()
        {
            /*
            string sql = "UPDATE Invoice SET Code = @Code WHERE InvoiceID = @InvoiceID";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var affectedRows = connection.Execute(sql,
                    new[]
                    {
                        new {InvoiceID = 1, Code = "Many_Update_1"},
                        new {InvoiceID = 2, Code = "Many_Update_2"},
                        new {InvoiceID = 3, Code = "Many_Update_3"}
                    });

                My.Result.Show(affectedRows);
            }
            */
        }

        /*
        Exemple - Exécuter DELETE

        Unique

        Exécutez l'instruction DELETE une seule fois.
        */
        private void Exemple011()
        {
            /*
            string sql = "DELETE FROM Invoice WHERE InvoiceID = @InvoiceID";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var affectedRows = connection.Execute(sql, new { InvoiceID = 1 });

                My.Result.Show(affectedRows);
            }
            */
        }
        /*
        Beaucoup

        Exécutez l'instruction DELETE plusieurs fois. Une fois pour chaque objet dans la liste du tableau.
        */

        private void Exemple012()
        {
            /*
            string sql = "DELETE FROM Invoice WHERE InvoiceID = @InvoiceID";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var affectedRows = connection.Execute(sql,
                    new[]
                    {
                        new {InvoiceID = 1},
                        new {InvoiceID = 2},
                        new {InvoiceID = 3}
                    });
            }
            */
        }

        /*

        La méthode de requête est une méthode d'extension qui peut être appelée depuis n'importe quel objet de type IDbConnection. 
        Il peut exécuter une requête et mapper le résultat.

        Le résultat peut être mappé à:

        Anonyme
        Fortement typé
        Multi-Mapping (One to One)
        Multi-mapping (One to Many)
        Multi-Type 

        Paramètres

        Le tableau suivant montre différents paramètres d'une méthode de requête.
        nom 	        description
        sql 	        La requête à exécuter.
        param 	        Les paramètres de la requête (default = null).
        transaction 	La transaction à utiliser (default = null).
        buffered 	    True pour lire les résultats de la requête (par défaut = true).
        commandTimeout 	La commande timeout (default = null)
        commandType 	Le type de commande (default = null)

        Exemple - Requête anonyme

        La requête SQL brute peut être exécutée à l'aide de la méthode de requête et mapper le résultat à une liste dynamique.
        */

        private void Exemple013()
        {
            /*
            string sql = "SELECT * FROM Invoice;";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var invoices = connection.Query(sql).ToList();

                My.Result.Show(invoices);
            }
            */
        }
        /*
        Requête anonyme
        Exemple - Requête fortement typée

        La requête SQL brute peut être exécutée à l'aide de la méthode de requête et mapper le résultat à une liste fortement typée.
        */
        private void Exemple014()
        {
            /*
            string sql = "SELECT * FROM Invoice;";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var invoices = connection.Query<Invoice>(sql).ToList();

                My.Result.Show(invoices);
            }
            */
        }
        /*
        Requête fortement typée
        Exemple - Interrogation multi-mappage (One to One)

        La requête SQL brute peut être exécutée à l'aide de la méthode de requête et mapper le résultat à une liste fortement typée avec une relation un à un.
        */
        private void Exemple015()
        {
            /*
            string sql = "SELECT * FROM Invoice AS A INNER JOIN InvoiceDetail AS B ON A.InvoiceID = B.InvoiceID;";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var invoices = connection.Query<Invoice, InvoiceDetail, Invoice>(
                        sql,
                        (invoice, invoiceDetail) =>
                        {
                            invoice.InvoiceDetail = invoiceDetail;
                            return invoice;
                        },
                        splitOn: "InvoiceID")
                    .Distinct()
                    .ToList();

                My.Result.Show(invoices);
            }
            */
        }
        /*        
        Exemple - Interrogation multi-mappING (One to Many)

        La requête SQL brute peut être exécutée à l'aide de la méthode Query et mapper le résultat sur une liste fortement typée avec une ou plusieurs relations.
        */
        private void Exemple016()
        {
            /*
            string sql = "SELECT * FROM Invoice AS A INNER JOIN InvoiceItem AS B ON A.InvoiceID = B.InvoiceID;";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var invoiceDictionary = new Dictionary<int, Invoice>();

                var invoices = connection.Query<Invoice, InvoiceItem, Invoice>(
                        sql,
                        (invoice, invoiceItem) =>
                        {
                            Invoice invoiceEntry;

                            if (!invoiceDictionary.TryGetValue(invoice.InvoiceID, out invoiceEntry))
                            {
                                invoiceEntry = invoice;
                                invoiceEntry.Items = new List<InvoiceItem>();
                                invoiceDictionary.Add(invoiceEntry.InvoiceID, invoiceEntry);
                            }

                            invoiceEntry.Items.Add(invoiceItem);
                            return invoiceEntry;
                        },
                        splitOn: "InvoiceID")
                    .Distinct()
                    .ToList();

                My.Result.Show(invoices);
            }
            */
        }
        /*        
        Exemple - Requête multi-type

        La requête SQL brute peut être exécutée à l'aide de la méthode de requête et mapper le résultat à une liste de différents types.
        */
        private void Exemple017()
        {
            /*
            string sql = "SELECT * FROM Invoice;";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var invoices = new List<Invoice>();

                using (var reader = connection.ExecuteReader(sql))
                {
                    var storeInvoiceParser = reader.GetRowParser<StoreInvoice>();
                    var webInvoiceParser = reader.GetRowParser<WebInvoice>();

                    while (reader.Read())
                    {
                        Invoice invoice;

                        switch ((InvoiceKind)reader.GetInt32(reader.GetOrdinal("Kind")))
                        {
                            case InvoiceKind.StoreInvoice:
                                invoice = storeInvoiceParser(reader);
                                break;
                            case InvoiceKind.WebInvoice:
                                invoice = webInvoiceParser(reader);
                                break;
                            default:
                                throw new Exception(ExceptionMessage.GeneralException);
                        }

                        invoices.Add(invoice);
                    }
                }

                My.Result.Show(invoices);
            }
            */
        }

        /*
        La méthode QueryFirst est une méthode d'extension qui peut être appelée depuis n'importe quel objet de type IDbConnection. 
        Il peut exécuter une requête et mapper le premier résultat.

        Le résultat peut être mappé à:

        Anonyme
        Fortement typé 

        Paramètres

        Le tableau suivant montre différents paramètres d'une méthode QueryFirst.
        nom 	        description
        ---------------------------
        sql 	        La requête à exécuter.
        param 	        Les paramètres de la requête (default = null).
        transaction 	La transaction à utiliser (default = null).
        commandTimeout 	La commande timeout (default = null)
        commandType 	Le type de commande (default = null)
        
        Première, unique et par défaut

        Faites attention à utiliser la bonne méthode. Les méthodes First & Single sont très différentes.
        Résultat 	        Pas d'objet 	Un article 	Beaucoup d'articles
        -------------------------------------------------------------------
        Premier 	        Exception 	    Article 	Premier article
        Unique 	            Exception 	    Article 	Exception
        FirstOrDefault 	    Défaut 	        Article 	Premier article
        SingleOrDefault 	Défaut 	        Article 	Exception
        
        Exemple - Requête anonyme

        Exécuter une requête et mapper le premier résultat à une liste dynamique.
        */
        private void Exemple018()
        {
            /*
            string sql = "SELECT * FROM Invoice WHERE InvoiceID = @InvoiceID;";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var invoice = connection.QueryFirst(sql, new { InvoiceID = 1 });
            }
            */
        }
        /*
        Exemple - Requête fortement typée

        Exécuter une requête et mapper le premier résultat à une liste fortement typée.
        */
        private void Exemple019()
        {
            /*
            string sql = "SELECT * FROM Invoice WHERE InvoiceID = @InvoiceID;";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var invoice = connection.QueryFirst<Invoice>(sql, new { InvoiceID = 1 });
            }
            */
        }

        /*

        La méthode QueryFirstOrDefault est une méthode d'extension qui peut être appelée depuis n'importe quel objet de type IDbConnection. 
        Il peut exécuter une requête et mapper le premier résultat, ou une valeur par défaut si la séquence ne contient aucun élément.

        Le résultat peut être mappé à:

            Anonyme
            Fortement typé 

        Paramètres

        Le tableau suivant montre différents paramètres d'une méthode QueryFirstOrDefault.
        nom 	        description
        ---------------------------
        sql 	        La requête à exécuter.
        param 	        Les paramètres de la requête (default = null).
        transaction 	La transaction à utiliser (default = null).
        commandTimeout 	La commande timeout (default = null)
        commandType 	Le type de commande (default = null)
        
        Première, unique et par défaut

        Faites attention à utiliser la bonne méthode. Les méthodes First & Single sont très différentes.
        Résultat 	        Pas d'objet 	Un article 	Beaucoup d'articles
        -------------------------------------------------------------------
        Premier 	        Exception 	    Article 	Premier article
        Unique 	            Exception 	    Article 	Exception
        FirstOrDefault 	    Défaut 	        Article 	Premier article
        SingleOrDefault 	Défaut 	        Article 	Exception
        
        Exemple - Requête anonyme

        Exécuter une requête et mapper le premier résultat à une liste dynamique ou une valeur par défaut si la séquence ne contient aucun élément.
        */

        private void Exemple020()
        {
            /*
            string sql = "SELECT * FROM Invoice WHERE InvoiceID = @InvoiceID;";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var invoice = connection.QueryFirstOrDefault(sql, new { InvoiceID = 1 });
            }
            */
        }
        /*
        Exemple - Requête fortement typée

        Exécuter une requête et mapper le premier résultat à une liste fortement typée, ou une valeur par défaut si la séquence ne contient aucun élément.
        */
        private void Exemple021()
        {
            /*
            string sql = "SELECT * FROM Invoice WHERE InvoiceID = @InvoiceID;";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var invoice = connection.QueryFirstOrDefault<Invoice>(sql, new { InvoiceID = 1 });
            }
            */
        }
        /*
        La méthode QuerySingle est une méthode d'extension qui peut être appelée depuis n'importe quel objet de type IDbConnection. 
        Il peut exécuter une requête et mapper le premier résultat et déclencher une exception s'il n'y a pas exactement un élément dans la séquence.

        Le résultat peut être mappé à:

            Anonyme
            Fortement typé 

        Paramètres

        Le tableau suivant montre le paramètre différent d'une méthode QuerySingle.
        nom 	        description
        sql 	        La requête à exécuter.
        param 	        Les paramètres de la requête (default = null).
        transaction 	La transaction à utiliser (default = null).
        commandTimeout 	La commande timeout (default = null)
        commandType 	Le type de commande (default = null)
        
        Première, unique et par défaut

        Faites attention à utiliser la bonne méthode. Les méthodes First & Single sont très différentes.
        Résultat 	        Pas d'objet 	Un article 	Beaucoup d'articles
        -------------------------------------------------------------------
        Premier 	        Exception 	    Article 	Premier article
        Unique 	            Exception 	    Article 	Exception
        FirstOrDefault 	    Défaut 	        Article 	Premier article
        SingleOrDefault 	Défaut 	        Article 	Exception
        Exemple - Requête anonyme

        Exécuter une requête et mapper le premier résultat à une liste dynamique et déclencher une exception s'il n'y a pas exactement un élément dans la séquence.
        */
        private void Exemple022()
        {
            /*
            string sql = "SELECT * FROM Invoice WHERE InvoiceID = @InvoiceID;";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var invoice = connection.QuerySingle(sql, new { InvoiceID = 1 });
            }
            */
        }
        /*
        Exemple - Requête fortement typée

        Exécuter une requête et mapper le premier résultat à une liste fortement typée, et déclencher une exception s'il n'y a pas exactement un élément dans la séquence.
        */
        private void Exemple023()
        {
            /*
            string sql = "SELECT * FROM Invoice WHERE InvoiceID = @InvoiceID;";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var invoice = connection.QuerySingle<Invoice>(sql, new { InvoiceID = 1 });
            }
            */
        }

        /*
        La méthode QuerySingleOrDefault est une méthode d'extension qui peut être appelée depuis n'importe quel objet de type IDbConnection. 
        Il peut exécuter une requête et mapper le premier résultat, ou une valeur par défaut si la séquence est vide; cette méthode déclenche une exception s'il y a plus d'un élément dans la séquence.

        Le résultat peut être mappé à:

            Anonyme
            Fortement tapé 

        Paramètres

        Le tableau suivant montre le paramètre différent d'une méthode QuerySingleOrDefault.
        nom 	        description
        ---------------------------
        sql 	        La requête à exécuter.
        param 	        Les paramètres de la requête (default = null).
        transaction 	La transaction à utiliser (default = null).
        commandTimeout 	La commande timeout (default = null)
        commandType 	Le type de commande (default = null)
        
        Première, unique et par défaut

        Faites attention à utiliser la bonne méthode. Les méthodes First & Single sont très différentes.
        Résultat 	    Pas d'objet 	Un article 	Beaucoup d'articles
        ---------------------------------------------------------------
        Premier 	    Exception 	    Article 	Premier article
        Unique 	        Exception 	    Article 	Exception
        FirstOrDefault 	Défaut 	        Article 	Premier article
        SingleOrDefault Défaut 	        Article 	Exception
                
        Exemple - Requête anonyme

        Exécuter une requête et mapper le premier résultat à une liste dynamique ou une valeur par défaut si la séquence est vide; cette méthode déclenche une exception s'il y a plus d'un élément dans la séquence.
        */
        private void Exemple024()
        {
            /*
            string sql = "SELECT * FROM Invoice WHERE InvoiceID = @InvoiceID;";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var invoice = connection.QuerySingleOrDefault(sql, new { InvoiceID = 1 });
            }
            */
        }
        /*
        Exemple - Requête fortement typée

        Exécuter une requête et mapper le premier résultat à une liste fortement typée, ou une valeur par défaut si la séquence est vide; cette méthode déclenche une exception s'il y a plus d'un élément dans la séquence.
        */
        private void Exemple025()
        {
            /*
            string sql = "SELECT * FROM Invoice WHERE InvoiceID = @InvoiceID;";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                var invoice = connection.QuerySingleOrDefault<Invoice>(sql, new { InvoiceID = 1 });
            }
            */
        }
        /*
        La méthode QueryMultiple est une méthode d'extension qui peut être appelée depuis n'importe quel objet de type IDbConnection. 
        Il peut exécuter plusieurs requêtes dans la même commande et mapper les résultats.
        */
        private void Exemple026()
        {
            /*
            string sql = "SELECT * FROM Invoice WHERE InvoiceID = @InvoiceID; SELECT * FROM InvoiceItem WHERE InvoiceID = @InvoiceID;";

            using (var connection = My.ConnectionFactory())
            {
                connection.Open();

                using (var multi = connection.QueryMultiple(sql, new { InvoiceID = 1 }))
                {
                    var invoice = multi.Read<Invoice>().First();
                    var invoiceItems = multi.Read<InvoiceItem>().ToList();
                }
            }
            */
        }
        /*
        Paramètres

        Le tableau suivant montre différents paramètres d'une méthode QueryMultiple.
        nom 	        description
        ---------------------------
        sql 	        La requête à exécuter.
        param 	        Les paramètres de la requête (default = null).
        transaction 	La transaction à utiliser (default = null).
        commandTimeout 	La commande timeout (default = null)
        commandType 	Le type de commande (default = null)

        */
    }
}
