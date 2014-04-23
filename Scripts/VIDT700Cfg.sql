SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

ALTER  VIEW VIDT700Cfg
as
select PosNo,ItemName,ItemValue 
from posCfg 
where  ItemName in ('RECEIPT HEAD1','RECEIPT HEAD2','RECEIPT HEAD3','RECEIPT TAIL1','RECEIPT TAIL2','RECEIPT TAIL3')
union 
select a.PosNo,'ICNEWMM',CsValue from Pos a ,XtSysCfg b where CsName='icnewmm'
union 
select a.PosNo,'IcLen',CsValue from Pos a,XtSysCfg b where CsName='zhlen'

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

