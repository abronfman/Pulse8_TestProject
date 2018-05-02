use Pulse8TestDB;
Go

IF not exists (Select 1 From INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME = 'member_getDiagnosisAndSeverity')
	EXEC sp_executesql N'Create procedure dbo.member_getDiagnosisAndSeverity as RAISERROR(''Procedure member_getDiagnosisAndSeverity is incomplete.'', 16, 127);'
Go

Alter procedure dbo.member_getDiagnosisAndSeverity (
	@MemberId INT
)
AS
BEGIN
	
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
Where m.MemberID = @MemberId
group by m.MemberID,m.FirstName, m.LastName, map.DiagnosisCategoryID, 
	cat.CategoryDescription, cat.CategoryScore, severity.mostSevereCategory
END 
GO
