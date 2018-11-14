<Query Kind="Expression">
  <Connection>
    <ID>1503ba49-825b-418a-8302-05871431344a</ID>
    <Persist>true</Persist>
    <Server>GHOST</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>internship</UserName>
    <Database>NLayerSample</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//3
Guests.Where(g => g.Age > 15)

//4
Guests.Where(g => g.Age >= 18 && g.Age <= 35).Select(g => new { g.FirstName, g.LastName }).OrderBy(g=>g.FirstName)

//5
Staffs.Where(s => s.LocationId == null).Select(s => new { s.FirstName, s.LastName, s.Email, s.Phone })

//6
Locations.Where(l => l.Address.Contains("Iasi"))