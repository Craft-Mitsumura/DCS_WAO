Option Strict Off
Option Explicit On
Module OracleConstantModule
	''''''''''''''''''''''''''''
	' Oracle Objects for OLE global constant file.
	' This file can be loaded into a code module.
	''''''''''''''''''''''''''''
	
	'Editmode property values
	' These are intended to match similar constants in the
	' Visual Basic file CONSTANT.TXT
	Public Const ORADATA_EDITNONE As Short = 0
	Public Const ORADATA_EDITMODE As Short = 1
	Public Const ORADATA_EDITADD As Short = 2
	
	' Field Data Types
	' These are intended to match similar constants in the
	' Visual Basic file DATACONS.TXT
	Public Const ORADB_BOOLEAN As Short = 1
	Public Const ORADB_BYTE As Short = 2
	Public Const ORADB_INTEGER As Short = 3
	Public Const ORADB_LONG As Short = 4
	Public Const ORADB_CURRENCY As Short = 5
	Public Const ORADB_SINGLE As Short = 6
	Public Const ORADB_DOUBLE As Short = 7
	Public Const ORADB_DATE As Short = 8
	Public Const ORADB_OBJECT As Short = 9
	Public Const ORADB_TEXT As Short = 10
	Public Const ORADB_LONGBINARY As Short = 11
	Public Const ORADB_MEMO As Short = 12
	
	'Parameter Types
	Public Const ORAPARM_INPUT As Short = 1
	Public Const ORAPARM_OUTPUT As Short = 2
	Public Const ORAPARM_BOTH As Short = 3
	
	'Parameter Status
	Public Const ORAPSTAT_INPUT As Integer = &H1
	Public Const ORAPSTAT_OUTPUT As Integer = &H2
	Public Const ORAPSTAT_AUTOENABLE As Integer = &H4
	Public Const ORAPSTAT_ENABLE As Integer = &H8
	
	'CreateDynaset Method Options
	Public Const ORADYN_DEFAULT As Integer = &H0
	Public Const ORADYN_NO_AUTOBIND As Integer = &H1
	Public Const ORADYN_NO_BLANKSTRIP As Integer = &H2
	Public Const ORADYN_READONLY As Integer = &H4
	Public Const ORADYN_NOCACHE As Integer = &H8
	Public Const ORADYN_ORAMODE As Integer = &H10
	Public Const ORADYN_NO_REFETCH As Integer = &H20
	Public Const ORADYN_NO_MOVEFIRST As Integer = &H40
	Public Const ORADYN_DIRTY_WRITE As Integer = &H80
	
	'OpenDatabase Method Options
	Public Const ORADB_DEFAULT As Integer = &H0
	Public Const ORADB_ORAMODE As Integer = &H1
	Public Const ORADB_NOWAIT As Integer = &H2
	Public Const ORADB_DBDEFAULT As Integer = &H4
	Public Const ORADB_DEFERRED As Integer = &H8
	Public Const ORADB_ENLIST_IN_MTS As Integer = &H9
	
	'Oracle type codes
	Public Const ORATYPE_VARCHAR2 As Short = 1
	Public Const ORATYPE_NUMBER As Short = 2
	Public Const ORATYPE_SINT As Short = 3
	Public Const ORATYPE_FLOAT As Short = 4
	Public Const ORATYPE_STRING As Short = 5
	Public Const ORATYPE_DECIMAL As Short = 7
	Public Const ORATYPE_VARCHAR As Short = 9
	Public Const ORATYPE_DATE As Short = 12
	Public Const ORATYPE_REAL As Short = 21
	Public Const ORATYPE_DOUBLE As Short = 22
	Public Const ORATYPE_UNSIGNED8 As Short = 23
	Public Const ORATYPE_UNSIGNED16 As Short = 25
	Public Const ORATYPE_UNSIGNED32 As Short = 26
	Public Const ORATYPE_SIGNED8 As Short = 27
	Public Const ORATYPE_SIGNED16 As Short = 28
	Public Const ORATYPE_SIGNED32 As Short = 29
	Public Const ORATYPE_PTR As Short = 32
	Public Const ORATYPE_OPAQUE As Short = 58
	Public Const ORATYPE_UINT As Short = 68
	Public Const ORATYPE_RAW As Short = 95
	Public Const ORATYPE_CHAR As Short = 96
	Public Const ORATYPE_CHARZ As Short = 97
	Public Const ORATYPE_CURSOR As Short = 102
	Public Const ORATYPE_ROWID As Short = 104
	Public Const ORATYPE_MLSLABEL As Short = 105
	Public Const ORATYPE_OBJECT As Short = 108
	Public Const ORATYPE_REF As Short = 110
	Public Const ORATYPE_CLOB As Short = 112
	Public Const ORATYPE_BLOB As Short = 113
	Public Const ORATYPE_BFILE As Short = 114
	Public Const ORATYPE_CFILE As Short = 115
	Public Const ORATYPE_RSLT As Short = 116
	Public Const ORATYPE_NAMEDCOLLECTION As Short = 122
	Public Const ORATYPE_COLL As Short = 122
	Public Const ORATYPE_SYSFIRST As Short = 228
	Public Const ORATYPE_SYSLAST As Short = 235
	Public Const ORATYPE_OCTET As Short = 245
	Public Const ORATYPE_SMALLINT As Short = 246
	Public Const ORATYPE_VARRAY As Short = 247
	Public Const ORATYPE_TABLE As Short = 248
	Public Const ORATYPE_OTMLAST As Short = 320
	
	
	'CreateSql Method options
	Public Const ORASQL_DEFAULT As Integer = &H0
	Public Const ORASQL_NO_AUTOBIND As Integer = &H1
	Public Const ORASQL_FAILEXEC As Integer = &H2
	
	'OraLob operation return codes
	Public Const ORALOB_SUCCESS As Short = 0
	Public Const ORALOB_NEED_DATA As Short = 99
	Public Const ORALOB_NODATA As Short = 100
	
	'OraLob Write operation chunck  modes
	Public Const ORALOB_ONE_PIECE As Short = 0
	Public Const ORALOB_FIRST_PIECE As Short = 1
	Public Const ORALOB_NEXT_PIECE As Short = 2
	Public Const ORALOB_LAST_PIECE As Short = 3
	
	'OraRef Lock operation
	Public Const ORAREF_NO_LOCK As Short = 1
	Public Const ORAREF_EXCLUSIVE_LOCK As Short = 2
	Public Const ORAREF_NOWAIT_LOCK As Short = 3
	
	'OraRef Pin operaion
	Public Const ORAREF_READ_ANY As Short = 3
	Public Const ORAREF_READ_RECENT As Short = 4
	Public Const ORAREF_READ_LATEST As Short = 5
	
	'OIP errors returned as part of the OLE Automation error.
	Public Const OERROR_ADVISEULINK As Short = 4096 ' Invalid advisory connection
	Public Const OERROR_POSITION As Short = 4098 ' Invalid database position
	Public Const OERROR_NOFIELDNAME As Short = 4099 ' Field 'field-name' not found
	Public Const OERROR_TRANSIP As Short = 4101 ' Transaction already in process
	Public Const OERROR_TRANSNIPC As Short = 4104 ' Commit detected with no active transaction
	Public Const OERROR_TRANSNIPR As Short = 4105 ' Rollback detected with no active transaction
	Public Const OERROR_NODSET As Short = 4106 ' No such set attached to connection
	Public Const OERROR_INVROWNUM As Short = 4108 ' Invalid row reference
	Public Const OERROR_TEMPFILE As Short = 4109 ' Error creating temporary file
	Public Const OERROR_DUPSESSION As Short = 4110 ' Duplicate session name
	Public Const OERROR_NOSESSION As Short = 4111 ' Session not found during detach
	Public Const OERROR_NOOBJECTN As Short = 4112 ' No such object named 'object-name'
	Public Const OERROR_DUPCONN As Short = 4113 ' Duplicate connection name
	Public Const OERROR_NOCONN As Short = 4114 ' No such connection during detach
	Public Const OERROR_BFINDEX As Short = 4115 ' Invalid field index
	Public Const OERROR_CURNREADY As Short = 4116 ' Cursor not ready for I/O
	Public Const OERROR_NOUPDATES As Short = 4117 ' Not an updatable set
	Public Const OERROR_NOTEDITING As Short = 4118 ' Attempt to update without edit or add operation
	Public Const OERROR_DATACHANGE As Short = 4119 ' Data has been modified
	Public Const OERROR_NOBUFMEM As Short = 4120 ' No memory for data transfer buffers
	Public Const OERROR_INVBKMRK As Short = 4121 ' Invalid bookmark
	Public Const OERROR_BNDVNOEN As Short = 4122 ' Bind variable not fully enabled
	Public Const OERROR_DUPPARAM As Short = 4123 ' Duplicate parameter name
	Public Const OERROR_INVARGVAL As Short = 4124 ' Invalid argument value
	Public Const OERROR_INVFLDTYPE As Short = 4125 ' Invalid field type
	Public Const OERROR_TRANSFORUP As Short = 4127 ' For Update detected with no active transaction
	Public Const OERROR_NOTUPFORUP As Short = 4128 ' For Update detected but not updatable set
	Public Const OERROR_TRANSLOCK As Short = 4129 ' Commit/Rollback with SELECT FOR UPDATE in progress
	Public Const OERROR_CACHEPARM As Short = 4130 ' Invalid cache parameter
	Public Const OERROR_FLDRQROWID As Short = 4131 ' Field processing requires ROWID
	Public Const OERROR_OUTOFMEMORY As Short = 4132 ' Internal Error
	Public Const OERROR_MAXSIZE As Short = 4135 ' Element size specified in AddTable exceeds the maximum allowed size for that variable type. See AddTable Method for more details.
	Public Const OERROR_INVDIMENSION As Short = 4136 ' Dimension specified in AddTable is invalid (i.e. negative). See AddTable Method for more details.
	Public Const OERROR_MAXBUFFER As Short = 4137 ' Buffer size for parameter array variable exceeds 32512 bytes (OCI limit).
	Public Const OERROR_ARRAYSIZ As Short = 4138 ' Dimensions of array parameters used in insert/update/delete statements are not equal.
	Public Const OERROR_ARRAYFAILP As Short = 4139 ' Error processing arrays. For details refer to OO4OERR.LOG in the windows directory.
	Public Const OERROR_CREATEPOOL As Short = 4147 ' Database Pool Already exists for this session.
	Public Const OERROR_GETDB As Short = 4148 ' Unable to obtain a free database object from the pool.
	
	Public Const OERROR_NOOBJECT As Short = 4796 'Creating Oracle object instance in client side object cache is failed
	Public Const OERROR_BINDERR As Short = 4797 'Binding  Oracle object instance to the SQL statement  is failed
	Public Const OERROR_NOATTRNAME As Short = 4798 'Getting attribute name of Oracle object instance is failed
	Public Const OERROR_NOATTRINDEX As Short = 4799 'Getting attribute index of Oracle object instance is failed
	Public Const OERROR_INVINPOBJECT As Short = 4801 'Invalid input object type for binding operation
	Public Const OERROR_BAD_INDICATOR As Short = 4802 'Fetched Oracle Object instance comes with invalid indicator structure
	Public Const OERROR_OBJINSTNULL As Short = 4803 'Operation on NULL Oracle object instance is failed. See IsNull property on OraObject
	Public Const OERROR_REFNULL As Short = 4804 'Pin Operation on NULL  Ref value is failed. See IsRefNull property on OraRef
	
	Public Const OERROR_INVPOLLPARAMS As Short = 4896 'Invalid  polling amount and chunksize specified for LOB read/write operation.
	Public Const OERROR_INVSEEKPARAMS As Short = 4897 'Invalid seek value is specified for LOB read/write operation.
	Public Const OERROR_LOBREAD As Short = 4898 'Read operation failed
	Public Const OERROR_LOBWRITE As Short = 4899 'Write operation failure
	Public Const OERROR_INVCLOBBUF As Short = 4900 'Input buffer type is not string for CLOB write operation
	Public Const OERROR_INVBLOBBUF As Short = 4901 'Input buffer type is not bytes for BLOB write operation
	Public Const OERROR_INVLOBLEN As Short = 4902 'Invalid buffer length for LOB write operation
	Public Const OERROR_NOEDIT As Short = 4903 'Write,Trim ,Append,Copy operation is allowed outside the dynaset edit
	Public Const OERROR_INVINPUTLOB As Short = 4904 'Invalid input LOB for bind operation
	Public Const OERROR_NOEDITONCLONE As Short = 4905 'Write,Trim,Append,Copy is not allowed for clone LOB object
	Public Const OERROR_LOBFILEOPEN As Short = 4906 'Specified file could not be opened in LOB operation
	Public Const OERROR_LOBFILEIOERR As Short = 4907 'File Read or Write failed in LOB Operation.
	Public Const OERROR_LOBNULL As Short = 4908 'Operation on NULL LOB has failed.
	
	Public Const OERROR_AQCREATEERR As Short = 4996 'Error creating AQ object
	Public Const OERROR_MSGCREATEERR As Short = 4997 'Error creating AQMsg object
	Public Const OERROR_PAYLOADCREATEERR As Short = 4998 ' Error creating Payload object
	Public Const OERROR_MAXAGENTS As Short = 4998 ' Maximum number of subscribers exceeded.
	Public Const OERROR_AGENTCREATEERR As Short = 5000 ' Error creating AQ Agent
	
	Public Const OERROR_COLLINSTNULL As Short = 5196 'Operation on NULL Oracle collection is  failed. See IsNull property on OraCollection
	Public Const OERROR_NOELEMENT As Short = 5197 'Element does not exist for given index
	Public Const OERROR_INVINDEX As Short = 5198 'Invalid collection index is specified
	Public Const OERROR_NODELETE As Short = 5199 'Delete operation is not supported for VARRAY collection type
	Public Const OERROR_SAFEARRINVELEM As Short = 5200 'Variant SafeArray cannot be created from the collection having non scalar element types
	
	Public Const OERROR_NULLNUMBER As Short = 5296 'Operation on NULL Oracle Number  is  failed.
	
	' meta data type, OraMetaData.type returns one of the following
	Public Const ORAMD_TABLE As Short = 1
	Public Const ORAMD_VIEW As Short = 2
	Public Const ORAMD_COLUMN As Short = 3
	Public Const ORAMD_COLUMN_LIST As Short = 4
	Public Const ORAMD_TYPE As Short = 5
	Public Const ORAMD_TYPE_ATTR As Short = 6
	Public Const ORAMD_TYPE_ATTR_LIST As Short = 7
	Public Const ORAMD_TYPE_METHOD As Short = 8
	Public Const ORAMD_TYPE_METHOD_LIST As Short = 9
	Public Const ORAMD_TYPE_ARG As Short = 10
	Public Const ORAMD_TYPE_RESULT As Short = 11
	Public Const ORAMD_PROC As Short = 12
	Public Const ORAMD_FUNC As Short = 13
	Public Const ORAMD_ARG As Short = 14
	Public Const ORAMD_ARG_LIST As Short = 15
	Public Const ORAMD_PACKAGE As Short = 16
	Public Const ORAMD_SUBPROG_LIST As Short = 17
	Public Const ORAMD_COLLECTION As Short = 18
	Public Const ORAMD_SYNONYM As Short = 19
	Public Const ORAMD_SEQENCE As Short = 20
	Public Const ORAMD_SCHEMA As Short = 21
	Public Const ORAMD_OBJECT_LIST As Short = 22
	Public Const ORAMD_SCHEMA_LIST As Short = 23
	Public Const ORAMD_DATABASE As Short = 24
	
	' AQ Options
	' AQ Visible options
	Public Const ORAAQ_ENQ_IMMEDIATE As Short = 1
	Public Const ORAAQ_ENQ_ON_COMMIT As Short = 2

    ' AQ MessageID options
    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
    'Public Const ORAAQ_NULL_MSGID As Object = System.DBNull.Value

    ' Selection Criteria for filtering messages
    Public Const ORAAQ_ANY As Short = 0
	Public Const ORAAQ_CONSUMER As Short = 1
	Public Const ORAAQ_MSGID As Short = 2
	
	' Locking behaviour while dequeueing messages
	Public Const ORAAQ_DQ_BROWSE As Short = 1
	Public Const ORAAQ_DQ_LOCKED As Short = 2
	Public Const ORAAQ_DQ_REMOVE As Short = 3
	
	' Message Position criteria for dequeuing
	Public Const ORAAQ_DQ_FIRST_MSG As Short = 1
	Public Const ORAAQ_DQ_NEXT_TRANS As Short = 2
	Public Const ORAAQ_DQ_NEXT_MSG As Short = 3
	
	' Wait options for a dequeue operation
	Public Const ORAAQ_DQ_WAIT_FOREVER As Short = -1
	Public Const ORAAQ_DQ_NOWAIT As Short = 0
	
	
	' Values of various OraAQMsg properties
	
	' Number of Seconds to delay a newly enqueued message
	' before it is available for dequeueing
	Public Const ORAAQ_MSG_NO_DELAY As Short = 0
	' Prioirity values for messages
	Public Const ORAAQ_MSG_PRIORITY_NORMAL As Short = 0
	Public Const ORAAQ_MSG_PRIORITY_HIGH As Short = -10
	Public Const ORAAQ_MSG_PRIORITY_LOW As Short = 10
	
	' Message Expiration in seconds
	Public Const ORAAQ_MSG_NO_EXPIRE As Short = 0
	Public Const ORAAQ_MAX_AGENTS As Short = 10
End Module