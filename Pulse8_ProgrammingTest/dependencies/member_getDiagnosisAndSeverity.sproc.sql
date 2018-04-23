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
	From Member m
		left outer join MemberDiagnosis md on m.MemberID = md.MemberID
		left outer join DiagnosisCategoryMap map on map.DiagnosisID = md.DiagnosisID
		left outer join DiagnosisCategory cat on map.DiagnosisCategoryID = cat.DiagnosisCategoryID
		left outer join MostSevereMemberDiagnosis severity on m.MemberID = severity.MemberID
			and md.DiagnosisID = severity.LowestDiagnosisID
		left outer join Diagnosis d on severity.LowestDiagnosisID = d.DiagnosisID
	Where m.MemberID = @MemberId
	Order by m.MemberID
END 
GO
