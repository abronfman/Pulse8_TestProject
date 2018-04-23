/*

Write a query to return a list of all members and all of their corresponding categories:
a. Please include the following fields: Member ID, First Name, Last Name, Most Severe Diagnosis ID, Most Severe Diagnosis Description, Category ID, Category Description, Category Score and Is Most Severe Category.
b. Most Severe Diagnosis ID and Description should be the diagnosis with the lowest Diagnosis ID present for each Member’s Category.
c. Is Most Severe Category – 0 or 1 should be set to 1 for the lowest Category ID present for each Member. Severity is not based on category score. Please also set this to 1 for Members without corresponding Categories.
d. Members without Diagnosis or Categories should be included in the result set.

*/


use Pulse8TestDB
Go

;WITH MostSevereMemberDiagnosis as (
	Select MemberId, MIN(DiagnosisID) as LowestDiagnosisID
	from MemberDiagnosis
	Group By MemberID
) 
Select m.MemberID,
	m.FirstName,
	m.LastName,
	severity.LowestDiagnosisID as MostSevereDiagnosisID,
	d.DiagnosisDescription as MostSevereDiagnosisDescription,
	cat.DiagnosisCategoryID as CategoryID,
	cat.CategoryDescription,
	cat.CategoryScore,
	case 
		when severity.LowestDiagnosisID is not null 
			then 1
		when severity.LowestDiagnosisID is null AND cat.DiagnosisCategoryID is null	
			then 1
		else 0
	end as IsMostSevereCategory
from Member m
left outer join MemberDiagnosis md on m.MemberID = md.MemberID
left outer join DiagnosisCategoryMap map on map.DiagnosisID = md.DiagnosisID
left outer join DiagnosisCategory cat on map.DiagnosisCategoryID = cat.DiagnosisCategoryID
left outer join MostSevereMemberDiagnosis severity on m.MemberID = severity.MemberID
	and md.DiagnosisID = severity.LowestDiagnosisID
left outer join Diagnosis d on severity.LowestDiagnosisID = d.DiagnosisID
where m.MemberID = 1
Order by m.MemberID

exec member_getDiagnosisAndSeverity @MemberId = 1

