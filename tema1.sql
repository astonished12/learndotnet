--3
select * from Guests where Age>=18;

--4
select g.FirstName,
	   g.LastName
 from Guests g
	   where g.Age>=18 and g.Age<=35
 order by g.FirstName;

--5
select s.FirstName,
	   s.LastName, 
	   s.Email,
	   s.Phone 
from Staffs s
	   where s.LocationId is null;

--6
select * from Locations where (Address like '%Iasi%' OR
								Address like '%Iași')

--7
select * from Staffs s 
		join StaffRoles sr on s.StaffRoleId = sr.Id 
		where (sr.Name like '%DJ%' OR
		      sr.Name like '%Photographer%' OR
			  sr.Name like '%Performer%')
			  and s.Fee >= 1500;


--8
select g.FirstName, 
	   g.LastName,
	   g.Email
 from Guests g
	  join EventGuests eg on eg.GuestId=g.Id 
	  join Events e on e.Id = eg.EventId  
 where e.Name ='Expert Network Christmas Party'  --and eg.ConfirmedAttendence=1
 order by g.FirstName, g.LastName;

 --9
SELECT TOP 5 * FROM Guests AS g
	JOIN EventGuests AS eg ON g.Id = eg.GuestId
	JOIN Events AS e ON eg.EventId = e.Id
	JOIN EventTypes AS et ON e.EventTypeId = et.Id
WHERE (et.Name = 'Wedding') AND (eg.HasAttended = 1)
ORDER BY eg.GiftAmount DESC

--10
select distinct l.Name, l.Address
from Locations l
	 join Events e on e.LocationId = l.Id 
	 where YEAR(e.StartTime) > YEAR(getdate());

--11
 select top 5 l.Id, l.Name, count(*)
 from Events e
	  join EventGuests eg on eg.EventId = e.Id 
	  join Guests g on g.Id = eg.GuestId
	  join Locations l on l.Id = e.LocationId
 where eg.HasAttended = 1
 GROUP BY l.Id, l.Name
 order by count(*) desc;

--12
select distinct g.Email, e.Name , e.Description 
from Events e
	  join EventGuests eg on eg.EventId = e.Id 
	  join Guests g on g.Id = eg.GuestId
	  join EventTypes et on et.Id = e.EventTypeId
where et.Name = 'Wedding' and eg.ConfirmedAttendence=1;

--13
select distinct l.Name
	  from Locations l
	  join Events e on e.LocationId = l.Id 
where (MONTH(e.StartTime)<5 or MONTH(e.StartTime)>9)
     and YEAR(e.StartTime) = 2019;


--14 
select TOP 1 CAST(MONTH(e.StartTime) AS CHAR(3)), count(*)
from Events e
	  GROUP BY MONTH(e.StartTime)
	  order by count(*) desc;

--15
select *
	 from Staffs s
	 join Locations l on s.LocationId = l.Id
	 join Events e on e.LocationId = l.Id
where e.Name = 'Best wedding ever';

--16
select sr.Name,
	   max(s.Fee), 
	   avg(s.Fee),
	   min(s.Fee) 
from Staffs s 
	   join StaffRoles sr on s.StaffRoleId = sr.Id
	   group by sr.Name ;

--17
select sum(s.Fee)+avg(l.RentFee)
from Staffs s 
	join Locations l on l.Id = s.LocationId
	join Events e on e.LocationId = l.Id
 where e.Name like 'Expert Network Christmas Party';  

--18
select e.Name, (sum(ISNULL(eg.GiftAmount, 0))- l.RentFee-sum(s.Fee))
from Events e
	  join EventGuests eg on eg.EventId = e.Id 
	  join EventTypes et on et.Id = e.EventTypeId
	  join Staffs s on s.LocationId = e.LocationId
	  join Locations l on l.Id = s.LocationId
 where et.Name = 'Wedding' and eg.HasAttended=1
 group by e.Name, l.RentFee


 --19 
 select e.Name,l.Name, count(*), ((sum(s.Fee)+l.RentFee)) / (count(s.Id) + count(eg.GuestId))
 from Events e 
	  join EventGuests eg on eg.EventId = e.Id 
	  join Staffs s on s.LocationId = e.LocationId
	  join Locations l on l.Id = s.LocationId
 where eg.HasAttended=1
 group by e.Name, l.Name, l.RentFee