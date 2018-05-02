/*
Write a query to return a list of all members and all of their corresponding categories:
a. Please include the following fields: Member ID, First Name, Last Name, Most Severe Diagnosis ID, Most Severe Diagnosis Description, Category ID, Category Description, Category Score and Is Most Severe Category.
b. Most Severe Diagnosis ID and Description should be the diagnosis with the lowest Diagnosis ID present for each Member’s Category.
c. Is Most Severe Category – 0 or 1 should be set to 1 for the lowest Category ID present for each Member. Severity is not based on category score. Please also set this to 1 for Members without corresponding Categories.
d. Members without Diagnosis or Categories should be included in the result set.

*/

use Pulse8TestDB
Go

Select m.MemberID, 
	m.FirstName, 
	m.LastName, 
	min(md.DiagnosisID) as MostSevereDiagnosisID,
	(	
		Select Diagnosis.DiagnosisDescription 
		from Diagnosis 
		where Diagnosis.DiagnosisID = min(md.DiagnosisId)
	) as MostSevereDiagnosisDescription, 
	map.DiagnosisCategoryID as CategoryID,
	cat.CategoryDescription,
	cat.CategoryScore,
		case 
			when severity.mostSevereCategory is not null then 1
			when severity.mostSevereCategory is null AND map.DiagnosisCategoryID is null then 1
			else 0
		end as IsMostSevere
from Member m
left outer join MemberDiagnosis md on m.MemberID= md.MemberID
left outer join DiagnosisCategoryMap map on md.DiagnosisID = map.DiagnosisID
left outer join DiagnosisCategory cat on map.DiagnosisCategoryID = cat.DiagnosisCategoryID
left outer join (
	select m.MemberID, min(map.DiagnosisCategoryID) mostSevereCategory
	from MemberDiagnosis m
	join DiagnosisCategoryMap map on m.DiagnosisID = map.DiagnosisID
	group by m.MemberID
) as severity 
	on m.MemberID = severity.MemberID	
		AND cat.DiagnosisCategoryID = severity.mostSevereCategory
group by m.MemberID,m.FirstName, m.LastName, map.DiagnosisCategoryID, 
	cat.CategoryDescription, cat.CategoryScore, severity.mostSevereCategory