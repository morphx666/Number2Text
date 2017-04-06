Imports System
Imports System.ComponentModel
Imports System.Management
Imports System.Collections
Imports System.Globalization
Imports System.ComponentModel.Design.Serialization
Imports System.Reflection

Namespace ROOT.CIMV2
    
    'Functions ShouldSerialize<PropertyName> are functions used by VS property browser to check if a particular property has to be serialized. These functions are added for all ValueType properties ( properties of type Int32, BOOL etc.. which cannot be set to null). These functions use Is<PropertyName>Null function. These functions are also used in the TypeConverter implementation for the properties to check for NULL value of property so that an empty value can be shown in Property browser in case of Drag and Drop in Visual studio.
    'Functions Is<PropertyName>Null() are used to check if a property is NULL.
    'Functions Reset<PropertyName> are added for Nullable Read/Write properties. These functions are used by VS designer in property browser to set a property to NULL.
    'Every property added to the class for WMI property has attributes set to define its behavior in Visual Studio designer and also to define a TypeConverter to be used.
    'Datetime conversion functions ToDateTime and ToDmtfDateTime are added to the class to convert DMTF datetime to System.DateTime and vice-versa.
    'An Early Bound class generated for the WMI class.Win32_LogicalDisk
    Public Class LogicalDisk
        Inherits System.ComponentModel.Component
        
        'Private property to hold the WMI namespace in which the class resides.
        Private Shared CreatedWmiNamespace As String = "ROOT\CIMV2"
        
        'Private property to hold the name of WMI class which created this class.
        Private Shared CreatedClassName As String = "Win32_LogicalDisk"
        
        'Private member variable to hold the ManagementScope which is used by the various methods.
        Private Shared statMgmtScope As System.Management.ManagementScope = Nothing
        
        Private PrivateSystemProperties As ManagementSystemProperties
        
        'Underlying lateBound WMI object.
        Private PrivateLateBoundObject As System.Management.ManagementObject
        
        'Member variable to store the 'automatic commit' behavior for the class.
        Private AutoCommitProp As Boolean
        
        'Private variable to hold the embedded property representing the instance.
        Private embeddedObj As System.Management.ManagementBaseObject
        
        'The current WMI object used
        Private curObj As System.Management.ManagementBaseObject
        
        'Flag to indicate if the instance is an embedded object.
        Private isEmbedded As Boolean
        
        'Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        Public Sub New()
            MyBase.New
            Me.InitializeObject(Nothing, Nothing, Nothing)
        End Sub
        
        Public Sub New(ByVal keyDeviceID As String)
            MyBase.New
            Me.InitializeObject(Nothing, New System.Management.ManagementPath(LogicalDisk.ConstructPath(keyDeviceID)), Nothing)
        End Sub
        
        Public Sub New(ByVal mgmtScope As System.Management.ManagementScope, ByVal keyDeviceID As String)
            MyBase.New
            Me.InitializeObject(CType(mgmtScope,System.Management.ManagementScope), New System.Management.ManagementPath(LogicalDisk.ConstructPath(keyDeviceID)), Nothing)
        End Sub
        
        Public Sub New(ByVal path As System.Management.ManagementPath, ByVal getOptions As System.Management.ObjectGetOptions)
            MyBase.New
            Me.InitializeObject(Nothing, path, getOptions)
        End Sub
        
        Public Sub New(ByVal mgmtScope As System.Management.ManagementScope, ByVal path As System.Management.ManagementPath)
            MyBase.New
            Me.InitializeObject(mgmtScope, path, Nothing)
        End Sub
        
        Public Sub New(ByVal path As System.Management.ManagementPath)
            MyBase.New
            Me.InitializeObject(Nothing, path, Nothing)
        End Sub
        
        Public Sub New(ByVal mgmtScope As System.Management.ManagementScope, ByVal path As System.Management.ManagementPath, ByVal getOptions As System.Management.ObjectGetOptions)
            MyBase.New
            Me.InitializeObject(mgmtScope, path, getOptions)
        End Sub
        
        Public Sub New(ByVal theObject As System.Management.ManagementObject)
            MyBase.New
            Initialize
            If (CheckIfProperClass(theObject) = true) Then
                PrivateLateBoundObject = theObject
                PrivateSystemProperties = New ManagementSystemProperties(PrivateLateBoundObject)
                curObj = PrivateLateBoundObject
            Else
                Throw New System.ArgumentException("Class name does not match.")
            End If
        End Sub
        
        Public Sub New(ByVal theObject As System.Management.ManagementBaseObject)
            MyBase.New
            Initialize
            If (CheckIfProperClass(theObject) = true) Then
                embeddedObj = theObject
                PrivateSystemProperties = New ManagementSystemProperties(theObject)
                curObj = embeddedObj
                isEmbedded = true
            Else
                Throw New System.ArgumentException("Class name does not match.")
            End If
        End Sub
        
        'Property returns the namespace of the WMI class.
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property OriginatingNamespace() As String
            Get
                Return "ROOT\CIMV2"
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property ManagementClassName() As String
            Get
                Dim strRet As String = CreatedClassName
                If (Not (curObj) Is Nothing) Then
                    If (Not (curObj.ClassPath) Is Nothing) Then
                        strRet = CType(curObj("__CLASS"),String)
                        If ((strRet Is Nothing)  _
                                    OrElse (strRet Is String.Empty)) Then
                            strRet = CreatedClassName
                        End If
                    End If
                End If
                Return strRet
            End Get
        End Property
        
        'Property pointing to an embedded object to get System properties of the WMI object.
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property SystemProperties() As ManagementSystemProperties
            Get
                Return PrivateSystemProperties
            End Get
        End Property
        
        'Property returning the underlying lateBound object.
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property LateBoundObject() As System.Management.ManagementBaseObject
            Get
                Return curObj
            End Get
        End Property
        
        'ManagementScope of the object.
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public Property Scope() As System.Management.ManagementScope
            Get
                If (isEmbedded = false) Then
                    Return PrivateLateBoundObject.Scope
                Else
                    Return Nothing
                End If
            End Get
            Set
                If (isEmbedded = false) Then
                    PrivateLateBoundObject.Scope = value
                End If
            End Set
        End Property
        
        'Property to show the commit behavior for the WMI object. If true, WMI object will be automatically saved after each property modification.(ie. Put() is called after modification of a property).
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public Property AutoCommit() As Boolean
            Get
                Return AutoCommitProp
            End Get
            Set
                AutoCommitProp = value
            End Set
        End Property
        
        'The ManagementPath of the underlying WMI object.
        <Browsable(true)>  _
        Public Property Path() As System.Management.ManagementPath
            Get
                If (isEmbedded = false) Then
                    Return PrivateLateBoundObject.Path
                Else
                    Return Nothing
                End If
            End Get
            Set
                If (isEmbedded = false) Then
                    If (CheckIfProperClass(Nothing, value, Nothing) <> true) Then
                        Throw New System.ArgumentException("Class name does not match.")
                    End If
                    PrivateLateBoundObject.Path = value
                End If
            End Set
        End Property
        
        'Public static scope property which is used by the various methods.
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public Shared Property StaticScope() As System.Management.ManagementScope
            Get
                Return statMgmtScope
            End Get
            Set
                statMgmtScope = value
            End Set
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsAccessNull() As Boolean
            Get
                If (curObj("Access") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("Access describes whether the media is readable (value=1), writeable (value=2), or"& _ 
            " both (value=3). ""Unknown"" (0) and ""Write Once"" (4) can also be defined."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property Access() As AccessValues
            Get
                If (curObj("Access") Is Nothing) Then
                    Return CType(System.Convert.ToInt32(5),AccessValues)
                End If
                Return CType(System.Convert.ToInt32(curObj("Access")),AccessValues)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsAvailabilityNull() As Boolean
            Get
                If (curObj("Availability") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The availability and status of the device.  For example, the Availability propert"& _ 
            "y indicates that the device is running and has full power (value=3), or is in a "& _ 
            "warning (4), test (5), degraded (10) or power save state (values 13-15 and 17). "& _ 
            "Regarding the power saving states, these are defined as follows: Value 13 (""Powe"& _ 
            "r Save - Unknown"") indicates that the device is known to be in a power save mode"& _ 
            ", but its exact status in this mode is unknown; 14 (""Power Save - Low Power Mode"& _ 
            """) indicates that the device is in a power save state but still functioning, and"& _ 
            " may exhibit degraded performance; 15 (""Power Save - Standby"") describes that th"& _ 
            "e device is not functioning but could be brought to full power 'quickly'; and va"& _ 
            "lue 17 (""Power Save - Warning"") indicates that the device is in a warning state,"& _ 
            " though also in a power save mode."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property Availability() As AvailabilityValues
            Get
                If (curObj("Availability") Is Nothing) Then
                    Return CType(System.Convert.ToInt32(0),AvailabilityValues)
                End If
                Return CType(System.Convert.ToInt32(curObj("Availability")),AvailabilityValues)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsBlockSizeNull() As Boolean
            Get
                If (curObj("BlockSize") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("Size in bytes of the blocks which form this StorageExtent. If variable block size"& _ 
            ", then the maximum block size in bytes should be specified. If the block size is"& _ 
            " unknown or if a block concept is not valid (for example, for Aggregate Extents,"& _ 
            " Memory or LogicalDisks), enter a 1."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property BlockSize() As ULong
            Get
                If (curObj("BlockSize") Is Nothing) Then
                    Return System.Convert.ToUInt64(0)
                End If
                Return CType(curObj("BlockSize"),ULong)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The Caption property is a short textual description (one-line string) of the obje"& _ 
            "ct.")>  _
        Public ReadOnly Property Caption() As String
            Get
                Return CType(curObj("Caption"),String)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsCompressedNull() As Boolean
            Get
                If (curObj("Compressed") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The Compressed property indicates whether the logical volume exists as a single c"& _ 
            "ompressed entity, such as a DoubleSpace volume.  If file based compression is su"& _ 
            "pported (such as on NTFS), this property will be FALSE."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property Compressed() As Boolean
            Get
                If (curObj("Compressed") Is Nothing) Then
                    Return System.Convert.ToBoolean(0)
                End If
                Return CType(curObj("Compressed"),Boolean)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsConfigManagerErrorCodeNull() As Boolean
            Get
                If (curObj("ConfigManagerErrorCode") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("Indicates the Win32 Configuration Manager error code.  The following values may b"& _ 
            "e returned: "&Global.Microsoft.VisualBasic.ChrW(10)&"0"&Global.Microsoft.VisualBasic.ChrW(9)&"This device is working properly. "&Global.Microsoft.VisualBasic.ChrW(10)&"1"&Global.Microsoft.VisualBasic.ChrW(9)&"This device is not configured"& _ 
            " correctly. "&Global.Microsoft.VisualBasic.ChrW(10)&"2"&Global.Microsoft.VisualBasic.ChrW(9)&"Windows cannot load the driver for this device. "&Global.Microsoft.VisualBasic.ChrW(10)&"3"&Global.Microsoft.VisualBasic.ChrW(9)&"The driver for"& _ 
            " this device might be corrupted, or your system may be running low on memory or "& _ 
            "other resources. "&Global.Microsoft.VisualBasic.ChrW(10)&"4"&Global.Microsoft.VisualBasic.ChrW(9)&"This device is not working properly. One of its drivers or y"& _ 
            "our registry might be corrupted. "&Global.Microsoft.VisualBasic.ChrW(10)&"5"&Global.Microsoft.VisualBasic.ChrW(9)&"The driver for this device needs a resource "& _ 
            "that Windows cannot manage. "&Global.Microsoft.VisualBasic.ChrW(10)&"6"&Global.Microsoft.VisualBasic.ChrW(9)&"The boot configuration for this device conflicts "& _ 
            "with other devices. "&Global.Microsoft.VisualBasic.ChrW(10)&"7"&Global.Microsoft.VisualBasic.ChrW(9)&"Cannot filter. "&Global.Microsoft.VisualBasic.ChrW(10)&"8"&Global.Microsoft.VisualBasic.ChrW(9)&"The driver loader for the device is mis"& _ 
            "sing. "&Global.Microsoft.VisualBasic.ChrW(10)&"9"&Global.Microsoft.VisualBasic.ChrW(9)&"This device is not working properly because the controlling firmware is"& _ 
            " reporting the resources for the device incorrectly. "&Global.Microsoft.VisualBasic.ChrW(10)&"10"&Global.Microsoft.VisualBasic.ChrW(9)&"This device cannot star"& _ 
            "t. "&Global.Microsoft.VisualBasic.ChrW(10)&"11"&Global.Microsoft.VisualBasic.ChrW(9)&"This device failed. "&Global.Microsoft.VisualBasic.ChrW(10)&"12"&Global.Microsoft.VisualBasic.ChrW(9)&"This device cannot find enough free resources tha"& _ 
            "t it can use. "&Global.Microsoft.VisualBasic.ChrW(10)&"13"&Global.Microsoft.VisualBasic.ChrW(9)&"Windows cannot verify this device's resources. "&Global.Microsoft.VisualBasic.ChrW(10)&"14"&Global.Microsoft.VisualBasic.ChrW(9)&"This device"& _ 
            " cannot work properly until you restart your computer. "&Global.Microsoft.VisualBasic.ChrW(10)&"15"&Global.Microsoft.VisualBasic.ChrW(9)&"This device is not wo"& _ 
            "rking properly because there is probably a re-enumeration problem. "&Global.Microsoft.VisualBasic.ChrW(10)&"16"&Global.Microsoft.VisualBasic.ChrW(9)&"Windows c"& _ 
            "annot identify all the resources this device uses. "&Global.Microsoft.VisualBasic.ChrW(10)&"17"&Global.Microsoft.VisualBasic.ChrW(9)&"This device is asking for"& _ 
            " an unknown resource type. "&Global.Microsoft.VisualBasic.ChrW(10)&"18"&Global.Microsoft.VisualBasic.ChrW(9)&"Reinstall the drivers for this device. "&Global.Microsoft.VisualBasic.ChrW(10)&"19"&Global.Microsoft.VisualBasic.ChrW(9)&"Your r"& _ 
            "egistry might be corrupted. "&Global.Microsoft.VisualBasic.ChrW(10)&"20"&Global.Microsoft.VisualBasic.ChrW(9)&"Failure using the VxD loader. "&Global.Microsoft.VisualBasic.ChrW(10)&"21"&Global.Microsoft.VisualBasic.ChrW(9)&"System failure"& _ 
            ": Try changing the driver for this device. If that does not work, see your hardw"& _ 
            "are documentation. Windows is removing this device. "&Global.Microsoft.VisualBasic.ChrW(10)&"22"&Global.Microsoft.VisualBasic.ChrW(9)&"This device is disabled."& _ 
            " "&Global.Microsoft.VisualBasic.ChrW(10)&"23"&Global.Microsoft.VisualBasic.ChrW(9)&"System failure: Try changing the driver for this device. If that doesn't wo"& _ 
            "rk, see your hardware documentation. "&Global.Microsoft.VisualBasic.ChrW(10)&"24"&Global.Microsoft.VisualBasic.ChrW(9)&"This device is not present, is not work"& _ 
            "ing properly, or does not have all its drivers installed. "&Global.Microsoft.VisualBasic.ChrW(10)&"25"&Global.Microsoft.VisualBasic.ChrW(9)&"Windows is still s"& _ 
            "etting up this device. "&Global.Microsoft.VisualBasic.ChrW(10)&"26"&Global.Microsoft.VisualBasic.ChrW(9)&"Windows is still setting up this device. "&Global.Microsoft.VisualBasic.ChrW(10)&"27"&Global.Microsoft.VisualBasic.ChrW(9)&"This dev"& _ 
            "ice does not have valid log configuration. "&Global.Microsoft.VisualBasic.ChrW(10)&"28"&Global.Microsoft.VisualBasic.ChrW(9)&"The drivers for this device are n"& _ 
            "ot installed. "&Global.Microsoft.VisualBasic.ChrW(10)&"29"&Global.Microsoft.VisualBasic.ChrW(9)&"This device is disabled because the firmware of the device did"& _ 
            " not give it the required resources. "&Global.Microsoft.VisualBasic.ChrW(10)&"30"&Global.Microsoft.VisualBasic.ChrW(9)&"This device is using an Interrupt Reque"& _ 
            "st (IRQ) resource that another device is using. "&Global.Microsoft.VisualBasic.ChrW(10)&"31"&Global.Microsoft.VisualBasic.ChrW(9)&"This device is not working p"& _ 
            "roperly because Windows cannot load the drivers required for this device."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property ConfigManagerErrorCode() As ConfigManagerErrorCodeValues
            Get
                If (curObj("ConfigManagerErrorCode") Is Nothing) Then
                    Return CType(System.Convert.ToInt32(32),ConfigManagerErrorCodeValues)
                End If
                Return CType(System.Convert.ToInt32(curObj("ConfigManagerErrorCode")),ConfigManagerErrorCodeValues)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsConfigManagerUserConfigNull() As Boolean
            Get
                If (curObj("ConfigManagerUserConfig") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("Indicates whether the device is using a user-defined configuration."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property ConfigManagerUserConfig() As Boolean
            Get
                If (curObj("ConfigManagerUserConfig") Is Nothing) Then
                    Return System.Convert.ToBoolean(0)
                End If
                Return CType(curObj("ConfigManagerUserConfig"),Boolean)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("CreationClassName indicates the name of the class or the subclass used in the cre"& _ 
            "ation of an instance. When used with the other key properties of this class, thi"& _ 
            "s property allows all instances of this class and its subclasses to be uniquely "& _ 
            "identified.")>  _
        Public ReadOnly Property CreationClassName() As String
            Get
                Return CType(curObj("CreationClassName"),String)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The Description property provides a textual description of the object. ")>  _
        Public ReadOnly Property Description() As String
            Get
                Return CType(curObj("Description"),String)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The DeviceID property contains a string uniquely identifying the logical disk fro"& _ 
            "m other devices on the system.")>  _
        Public ReadOnly Property DeviceID() As String
            Get
                Return CType(curObj("DeviceID"),String)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsDriveTypeNull() As Boolean
            Get
                If (curObj("DriveType") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The DriveType property contains a numeric value corresponding to the type of disk"& _ 
            " drive this logical disk represents.  Please refer to the Platform SDK documenta"& _ 
            "tion for additional values."&Global.Microsoft.VisualBasic.ChrW(10)&"Example: A CD-ROM drive would return 5."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property DriveType() As DriveTypeValues
            Get
                If (curObj("DriveType") Is Nothing) Then
                    Return CType(System.Convert.ToInt32(7),DriveTypeValues)
                End If
                Return CType(System.Convert.ToInt32(curObj("DriveType")),DriveTypeValues)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsErrorClearedNull() As Boolean
            Get
                If (curObj("ErrorCleared") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("ErrorCleared is a boolean property indicating that the error reported in LastErro"& _ 
            "rCode property is now cleared."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property ErrorCleared() As Boolean
            Get
                If (curObj("ErrorCleared") Is Nothing) Then
                    Return System.Convert.ToBoolean(0)
                End If
                Return CType(curObj("ErrorCleared"),Boolean)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("ErrorDescription is a free-form string supplying more information about the error"& _ 
            " recorded in LastErrorCode property, and information on any corrective actions t"& _ 
            "hat may be taken.")>  _
        Public ReadOnly Property ErrorDescription() As String
            Get
                Return CType(curObj("ErrorDescription"),String)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("ErrorMethodology is a free-form string describing the type of error detection and"& _ 
            " correction supported by this storage extent.")>  _
        Public ReadOnly Property ErrorMethodology() As String
            Get
                Return CType(curObj("ErrorMethodology"),String)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The FileSystem property indicates the file system on the logical disk."&Global.Microsoft.VisualBasic.ChrW(10)&"Example: N"& _ 
            "TFS")>  _
        Public ReadOnly Property FileSystem() As String
            Get
                Return CType(curObj("FileSystem"),String)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsFreeSpaceNull() As Boolean
            Get
                If (curObj("FreeSpace") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The FreeSpace property indicates in bytes how much free space is available on the"& _ 
            " logical disk."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property FreeSpace() As ULong
            Get
                If (curObj("FreeSpace") Is Nothing) Then
                    Return System.Convert.ToUInt64(0)
                End If
                Return CType(curObj("FreeSpace"),ULong)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsInstallDateNull() As Boolean
            Get
                If (curObj("InstallDate") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The InstallDate property is datetime value indicating when the object was install"& _ 
            "ed. A lack of a value does not indicate that the object is not installed."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property InstallDate() As Date
            Get
                If (Not (curObj("InstallDate")) Is Nothing) Then
                    Return ToDateTime(CType(curObj("InstallDate"),String))
                Else
                    Return Date.MinValue
                End If
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsLastErrorCodeNull() As Boolean
            Get
                If (curObj("LastErrorCode") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("LastErrorCode captures the last error code reported by the logical device."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property LastErrorCode() As UInteger
            Get
                If (curObj("LastErrorCode") Is Nothing) Then
                    Return System.Convert.ToUInt32(0)
                End If
                Return CType(curObj("LastErrorCode"),UInteger)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsMaximumComponentLengthNull() As Boolean
            Get
                If (curObj("MaximumComponentLength") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The MaximumComponentLength property contains the maximum length of a filename com"& _ 
            "ponent supported by the Win32 drive. A filename component is that portion of a f"& _ 
            "ilename between backslashes.  The value can be used to indicate that long names "& _ 
            "are supported by the specified file system. For example, for a FAT file system s"& _ 
            "upporting long names, the function stores the value 255, rather than the previou"& _ 
            "s 8.3 indicator. Long names can also be supported on systems that use the NTFS f"& _ 
            "ile system."&Global.Microsoft.VisualBasic.ChrW(10)&"Example: 255"),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property MaximumComponentLength() As UInteger
            Get
                If (curObj("MaximumComponentLength") Is Nothing) Then
                    Return System.Convert.ToUInt32(0)
                End If
                Return CType(curObj("MaximumComponentLength"),UInteger)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsMediaTypeNull() As Boolean
            Get
                If (curObj("MediaType") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The MediaType property indicates the type of media currently present in the logic"& _ 
            "al drive. This value will be one of the values of the MEDIA_TYPE enumeration def"& _ 
            "ined in winioctl.h."&Global.Microsoft.VisualBasic.ChrW(10)&"<B>Note:</B> The value may not be exact for removable drives"& _ 
            " if currently there is no media in the drive."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property MediaType() As MediaTypeValues
            Get
                If (curObj("MediaType") Is Nothing) Then
                    Return CType(System.Convert.ToInt32(23),MediaTypeValues)
                End If
                Return CType(System.Convert.ToInt32(curObj("MediaType")),MediaTypeValues)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The Name property defines the label by which the object is known. When subclassed"& _ 
            ", the Name property can be overridden to be a Key property.")>  _
        Public ReadOnly Property Name() As String
            Get
                Return CType(curObj("Name"),String)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsNumberOfBlocksNull() As Boolean
            Get
                If (curObj("NumberOfBlocks") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("Total number of consecutive blocks, each block the size of the value contained in"& _ 
            " the BlockSize property, which form this storage extent. Total size of the stora"& _ 
            "ge extent can be calculated by multiplying the value of the BlockSize property b"& _ 
            "y the value of this property. If the value of BlockSize is 1, this property is t"& _ 
            "he total size of the storage extent."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property NumberOfBlocks() As ULong
            Get
                If (curObj("NumberOfBlocks") Is Nothing) Then
                    Return System.Convert.ToUInt64(0)
                End If
                Return CType(curObj("NumberOfBlocks"),ULong)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("Indicates the Win32 Plug and Play device ID of the logical device.  Example: *PNP"& _ 
            "030b")>  _
        Public ReadOnly Property PNPDeviceID() As String
            Get
                Return CType(curObj("PNPDeviceID"),String)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("Indicates the specific power-related capabilities of the logical device. The arra"& _ 
            "y values, 0=""Unknown"", 1=""Not Supported"" and 2=""Disabled"" are self-explanatory. "& _ 
            "The value, 3=""Enabled"" indicates that the power management features are currentl"& _ 
            "y enabled but the exact feature set is unknown or the information is unavailable"& _ 
            ". ""Power Saving Modes Entered Automatically"" (4) describes that a device can cha"& _ 
            "nge its power state based on usage or other criteria. ""Power State Settable"" (5)"& _ 
            " indicates that the SetPowerState method is supported. ""Power Cycling Supported"""& _ 
            " (6) indicates that the SetPowerState method can be invoked with the PowerState "& _ 
            "input variable set to 5 (""Power Cycle""). ""Timed Power On Supported"" (7) indicate"& _ 
            "s that the SetPowerState method can be invoked with the PowerState input variabl"& _ 
            "e set to 5 (""Power Cycle"") and the Time parameter set to a specific date and tim"& _ 
            "e, or interval, for power-on.")>  _
        Public ReadOnly Property PowerManagementCapabilities() As PowerManagementCapabilitiesValues()
            Get
                Dim arrEnumVals As System.Array = CType(curObj("PowerManagementCapabilities"),System.Array)
                Dim enumToRet((arrEnumVals.Length) - 1) As PowerManagementCapabilitiesValues
                Dim counter As Integer = 0
                counter = 0
                Do While (counter < arrEnumVals.Length)
                    enumToRet(counter) = CType(System.Convert.ToInt32(arrEnumVals.GetValue(counter)),PowerManagementCapabilitiesValues)
                    counter = (counter + 1)
                Loop
                Return enumToRet
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsPowerManagementSupportedNull() As Boolean
            Get
                If (curObj("PowerManagementSupported") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("Boolean indicating that the Device can be power managed - ie, put into a power sa"& _ 
            "ve state. This boolean does not indicate that power management features are curr"& _ 
            "ently enabled, or if enabled, what features are supported. Refer to the PowerMan"& _ 
            "agementCapabilities array for this information. If this boolean is false, the in"& _ 
            "teger value 1, for the string, ""Not Supported"", should be the only entry in the "& _ 
            "PowerManagementCapabilities array."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property PowerManagementSupported() As Boolean
            Get
                If (curObj("PowerManagementSupported") Is Nothing) Then
                    Return System.Convert.ToBoolean(0)
                End If
                Return CType(curObj("PowerManagementSupported"),Boolean)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The ProviderName property indicates the network path name to the logical device.")>  _
        Public ReadOnly Property ProviderName() As String
            Get
                Return CType(curObj("ProviderName"),String)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("A free form string describing the media and/or its use.")>  _
        Public ReadOnly Property Purpose() As String
            Get
                Return CType(curObj("Purpose"),String)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsQuotasDisabledNull() As Boolean
            Get
                If (curObj("QuotasDisabled") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The QuotasDisabled property indicates that Quota management is not enabled on thi"& _ 
            "s volume."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public Property QuotasDisabled() As Boolean
            Get
                If (curObj("QuotasDisabled") Is Nothing) Then
                    Return System.Convert.ToBoolean(0)
                End If
                Return CType(curObj("QuotasDisabled"),Boolean)
            End Get
            Set
                curObj("QuotasDisabled") = value
                If ((isEmbedded = false)  _
                            AndAlso (AutoCommitProp = true)) Then
                    PrivateLateBoundObject.Put
                End If
            End Set
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsQuotasIncompleteNull() As Boolean
            Get
                If (curObj("QuotasIncomplete") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The QuotasIncomplete property indicates that Quota management was used but has be"& _ 
            "en disabled.  Incomplete refers to the information left in the file system  afte"& _ 
            "r quota management has been disabled."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property QuotasIncomplete() As Boolean
            Get
                If (curObj("QuotasIncomplete") Is Nothing) Then
                    Return System.Convert.ToBoolean(0)
                End If
                Return CType(curObj("QuotasIncomplete"),Boolean)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsQuotasRebuildingNull() As Boolean
            Get
                If (curObj("QuotasRebuilding") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The QuotasRebuilding property indicates an active state signifying that the file "& _ 
            "system is in process of compiling information and setting the disk up for quota "& _ 
            "management."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property QuotasRebuilding() As Boolean
            Get
                If (curObj("QuotasRebuilding") Is Nothing) Then
                    Return System.Convert.ToBoolean(0)
                End If
                Return CType(curObj("QuotasRebuilding"),Boolean)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsSizeNull() As Boolean
            Get
                If (curObj("Size") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The Size property indicates in bytes, the size of the logical disk."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property Size() As ULong
            Get
                If (curObj("Size") Is Nothing) Then
                    Return System.Convert.ToUInt64(0)
                End If
                Return CType(curObj("Size"),ULong)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The Status property is a string indicating the current status of the object. Vari"& _ 
            "ous operational and non-operational statuses can be defined. Operational statuse"& _ 
            "s are ""OK"", ""Degraded"" and ""Pred Fail"". ""Pred Fail"" indicates that an element ma"& _ 
            "y be functioning properly but predicting a failure in the near future. An exampl"& _ 
            "e is a SMART-enabled hard drive. Non-operational statuses can also be specified."& _ 
            " These are ""Error"", ""Starting"", ""Stopping"" and ""Service"". The latter, ""Service"","& _ 
            " could apply during mirror-resilvering of a disk, reload of a user permissions l"& _ 
            "ist, or other administrative work. Not all such work is on-line, yet the managed"& _ 
            " element is neither ""OK"" nor in one of the other states.")>  _
        Public ReadOnly Property Status() As String
            Get
                Return CType(curObj("Status"),String)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsStatusInfoNull() As Boolean
            Get
                If (curObj("StatusInfo") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("StatusInfo is a string indicating whether the logical device is in an enabled (va"& _ 
            "lue = 3), disabled (value = 4) or some other (1) or unknown (2) state. If this p"& _ 
            "roperty does not apply to the logical device, the value, 5 (""Not Applicable""), s"& _ 
            "hould be used."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property StatusInfo() As StatusInfoValues
            Get
                If (curObj("StatusInfo") Is Nothing) Then
                    Return CType(System.Convert.ToInt32(0),StatusInfoValues)
                End If
                Return CType(System.Convert.ToInt32(curObj("StatusInfo")),StatusInfoValues)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsSupportsDiskQuotasNull() As Boolean
            Get
                If (curObj("SupportsDiskQuotas") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The SupportsDiskQuotas property indicates whether this volume supports disk Quota"& _ 
            "s"),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property SupportsDiskQuotas() As Boolean
            Get
                If (curObj("SupportsDiskQuotas") Is Nothing) Then
                    Return System.Convert.ToBoolean(0)
                End If
                Return CType(curObj("SupportsDiskQuotas"),Boolean)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsSupportsFileBasedCompressionNull() As Boolean
            Get
                If (curObj("SupportsFileBasedCompression") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The SupportsFileBasedCompression property indicates whether the logical disk part"& _ 
            "ition supports file based compression, such as is the case with NTFS. This prope"& _ 
            "rty is FALSE, when the Compressed property is TRUE."&Global.Microsoft.VisualBasic.ChrW(10)&"Values: TRUE or FALSE. If TR"& _ 
            "UE, the logical disk supports file based compression."),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property SupportsFileBasedCompression() As Boolean
            Get
                If (curObj("SupportsFileBasedCompression") Is Nothing) Then
                    Return System.Convert.ToBoolean(0)
                End If
                Return CType(curObj("SupportsFileBasedCompression"),Boolean)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The scoping System's CreationClassName.")>  _
        Public ReadOnly Property SystemCreationClassName() As String
            Get
                Return CType(curObj("SystemCreationClassName"),String)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The scoping System's Name.")>  _
        Public ReadOnly Property SystemName() As String
            Get
                Return CType(curObj("SystemName"),String)
            End Get
        End Property
        
        <Browsable(false),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>  _
        Public ReadOnly Property IsVolumeDirtyNull() As Boolean
            Get
                If (curObj("VolumeDirty") Is Nothing) Then
                    Return true
                Else
                    Return false
                End If
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The VolumeDirty property indicates whether the disk requires chkdsk to be run at "& _ 
            "next boot up time. The property is applicable to only those instances of logical"& _ 
            " disk that represent a physical disk in the machine. It is not applicable to map"& _ 
            "ped logical drives. "),  _
         TypeConverter(GetType(WMIValueTypeConverter))>  _
        Public ReadOnly Property VolumeDirty() As Boolean
            Get
                If (curObj("VolumeDirty") Is Nothing) Then
                    Return System.Convert.ToBoolean(0)
                End If
                Return CType(curObj("VolumeDirty"),Boolean)
            End Get
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The VolumeName property indicates the volume name of the logical disk."&Global.Microsoft.VisualBasic.ChrW(10)&"Constraint"& _ 
            "s: Maximum 32 characters")>  _
        Public Property VolumeName() As String
            Get
                Return CType(curObj("VolumeName"),String)
            End Get
            Set
                curObj("VolumeName") = value
                If ((isEmbedded = false)  _
                            AndAlso (AutoCommitProp = true)) Then
                    PrivateLateBoundObject.Put
                End If
            End Set
        End Property
        
        <Browsable(true),  _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),  _
         Description("The VolumeSerialNumber property indicates the volume serial number of the logical"& _ 
            " disk."&Global.Microsoft.VisualBasic.ChrW(10)&"Constraints: Maximum 11 characters"&Global.Microsoft.VisualBasic.ChrW(10)&"Example: A8C3-D032")>  _
        Public ReadOnly Property VolumeSerialNumber() As String
            Get
                Return CType(curObj("VolumeSerialNumber"),String)
            End Get
        End Property
        
        Private Overloads Function CheckIfProperClass(ByVal mgmtScope As System.Management.ManagementScope, ByVal path As System.Management.ManagementPath, ByVal OptionsParam As System.Management.ObjectGetOptions) As Boolean
            If ((Not (path) Is Nothing)  _
                        AndAlso (String.Compare(path.ClassName, Me.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) = 0)) Then
                Return true
            Else
                Return CheckIfProperClass(New System.Management.ManagementObject(mgmtScope, path, OptionsParam))
            End If
        End Function
        
        Private Overloads Function CheckIfProperClass(ByVal theObj As System.Management.ManagementBaseObject) As Boolean
            If ((Not (theObj) Is Nothing)  _
                        AndAlso (String.Compare(CType(theObj("__CLASS"),String), Me.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) = 0)) Then
                Return true
            Else
                Dim parentClasses As System.Array = CType(theObj("__DERIVATION"),System.Array)
                If (Not (parentClasses) Is Nothing) Then
                    Dim count As Integer = 0
                    count = 0
                    Do While (count < parentClasses.Length)
                        If (String.Compare(CType(parentClasses.GetValue(count),String), Me.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) = 0) Then
                            Return true
                        End If
                        count = (count + 1)
                    Loop
                End If
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeAccess() As Boolean
            If (Me.IsAccessNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeAvailability() As Boolean
            If (Me.IsAvailabilityNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeBlockSize() As Boolean
            If (Me.IsBlockSizeNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeCompressed() As Boolean
            If (Me.IsCompressedNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeConfigManagerErrorCode() As Boolean
            If (Me.IsConfigManagerErrorCodeNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeConfigManagerUserConfig() As Boolean
            If (Me.IsConfigManagerUserConfigNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeDriveType() As Boolean
            If (Me.IsDriveTypeNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeErrorCleared() As Boolean
            If (Me.IsErrorClearedNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeFreeSpace() As Boolean
            If (Me.IsFreeSpaceNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        'Converts a given datetime in DMTF format to System.DateTime object.
        Shared Function ToDateTime(ByVal dmtfDate As String) As Date
            Dim initializer As Date = Date.MinValue
            Dim year As Integer = initializer.Year
            Dim month As Integer = initializer.Month
            Dim day As Integer = initializer.Day
            Dim hour As Integer = initializer.Hour
            Dim minute As Integer = initializer.Minute
            Dim second As Integer = initializer.Second
            Dim ticks As Long = 0
            Dim dmtf As String = dmtfDate
            Dim datetime As Date = Date.MinValue
            Dim tempString As String = String.Empty
            If (dmtf Is Nothing) Then
                Throw New System.ArgumentOutOfRangeException
            End If
            If (dmtf.Length = 0) Then
                Throw New System.ArgumentOutOfRangeException
            End If
            If (dmtf.Length <> 25) Then
                Throw New System.ArgumentOutOfRangeException
            End If
            Try 
                tempString = dmtf.Substring(0, 4)
                If ("****" <> tempString) Then
                    year = Integer.Parse(tempString)
                End If
                tempString = dmtf.Substring(4, 2)
                If ("**" <> tempString) Then
                    month = Integer.Parse(tempString)
                End If
                tempString = dmtf.Substring(6, 2)
                If ("**" <> tempString) Then
                    day = Integer.Parse(tempString)
                End If
                tempString = dmtf.Substring(8, 2)
                If ("**" <> tempString) Then
                    hour = Integer.Parse(tempString)
                End If
                tempString = dmtf.Substring(10, 2)
                If ("**" <> tempString) Then
                    minute = Integer.Parse(tempString)
                End If
                tempString = dmtf.Substring(12, 2)
                If ("**" <> tempString) Then
                    second = Integer.Parse(tempString)
                End If
                tempString = dmtf.Substring(15, 6)
                If ("******" <> tempString) Then
                    ticks = (Long.Parse(tempString) * CType((System.TimeSpan.TicksPerMillisecond / 1000),Long))
                End If
                If ((((((((year < 0)  _
                            OrElse (month < 0))  _
                            OrElse (day < 0))  _
                            OrElse (hour < 0))  _
                            OrElse (minute < 0))  _
                            OrElse (minute < 0))  _
                            OrElse (second < 0))  _
                            OrElse (ticks < 0)) Then
                    Throw New System.ArgumentOutOfRangeException
                End If
            Catch e As System.Exception
                Throw New System.ArgumentOutOfRangeException(Nothing, e.Message)
            End Try
            datetime = New Date(year, month, day, hour, minute, second, 0)
            datetime = datetime.AddTicks(ticks)
            Dim tickOffset As System.TimeSpan = System.TimeZone.CurrentTimeZone.GetUtcOffset(datetime)
            Dim UTCOffset As Integer = 0
            Dim OffsetToBeAdjusted As Integer = 0
            Dim OffsetMins As Long = CType((tickOffset.Ticks / System.TimeSpan.TicksPerMinute),Long)
            tempString = dmtf.Substring(22, 3)
            If (tempString <> "******") Then
                tempString = dmtf.Substring(21, 4)
                Try 
                    UTCOffset = Integer.Parse(tempString)
                Catch e As System.Exception
                    Throw New System.ArgumentOutOfRangeException(Nothing, e.Message)
                End Try
                OffsetToBeAdjusted = CType((OffsetMins - UTCOffset),Integer)
                datetime = datetime.AddMinutes(CType(OffsetToBeAdjusted,Double))
            End If
            Return datetime
        End Function
        
        'Converts a given System.DateTime object to DMTF datetime format.
        Shared Function ToDmtfDateTime(ByVal [date] As Date) As String
            Dim utcString As String = String.Empty
            Dim tickOffset As System.TimeSpan = System.TimeZone.CurrentTimeZone.GetUtcOffset([date])
            Dim OffsetMins As Long = CType((tickOffset.Ticks / System.TimeSpan.TicksPerMinute),Long)
            If (System.Math.Abs(OffsetMins) > 999) Then
                [date] = [date].ToUniversalTime
                utcString = "+000"
            Else
                If (tickOffset.Ticks >= 0) Then
                    utcString = String.Concat("+", CType((tickOffset.Ticks / System.TimeSpan.TicksPerMinute),System.Int64 ).ToString.PadLeft(3, Global.Microsoft.VisualBasic.ChrW(48)))
                Else
                    Dim strTemp As String = CType(OffsetMins,System.Int64 ).ToString
                    utcString = String.Concat("-", strTemp.Substring(1, (strTemp.Length - 1)).PadLeft(3, Global.Microsoft.VisualBasic.ChrW(48)))
                End If
            End If
            Dim dmtfDateTime As String = CType([date].Year,System.Int32 ).ToString.PadLeft(4, Global.Microsoft.VisualBasic.ChrW(48))
            dmtfDateTime = String.Concat(dmtfDateTime, CType([date].Month,System.Int32 ).ToString.PadLeft(2, Global.Microsoft.VisualBasic.ChrW(48)))
            dmtfDateTime = String.Concat(dmtfDateTime, CType([date].Day,System.Int32 ).ToString.PadLeft(2, Global.Microsoft.VisualBasic.ChrW(48)))
            dmtfDateTime = String.Concat(dmtfDateTime, CType([date].Hour,System.Int32 ).ToString.PadLeft(2, Global.Microsoft.VisualBasic.ChrW(48)))
            dmtfDateTime = String.Concat(dmtfDateTime, CType([date].Minute,System.Int32 ).ToString.PadLeft(2, Global.Microsoft.VisualBasic.ChrW(48)))
            dmtfDateTime = String.Concat(dmtfDateTime, CType([date].Second,System.Int32 ).ToString.PadLeft(2, Global.Microsoft.VisualBasic.ChrW(48)))
            dmtfDateTime = String.Concat(dmtfDateTime, ".")
            Dim dtTemp As Date = New Date([date].Year, [date].Month, [date].Day, [date].Hour, [date].Minute, [date].Second, 0)
            Dim microsec As Long = CType(((([date].Ticks - dtTemp.Ticks)  _
                        * 1000)  _
                        / System.TimeSpan.TicksPerMillisecond),Long)
            Dim strMicrosec As String = CType(microsec,System.Int64 ).ToString
            If (strMicrosec.Length > 6) Then
                strMicrosec = strMicrosec.Substring(0, 6)
            End If
            dmtfDateTime = String.Concat(dmtfDateTime, strMicrosec.PadLeft(6, Global.Microsoft.VisualBasic.ChrW(48)))
            dmtfDateTime = String.Concat(dmtfDateTime, utcString)
            Return dmtfDateTime
        End Function
        
        Private Function ShouldSerializeInstallDate() As Boolean
            If (Me.IsInstallDateNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeLastErrorCode() As Boolean
            If (Me.IsLastErrorCodeNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeMaximumComponentLength() As Boolean
            If (Me.IsMaximumComponentLengthNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeMediaType() As Boolean
            If (Me.IsMediaTypeNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeNumberOfBlocks() As Boolean
            If (Me.IsNumberOfBlocksNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializePowerManagementSupported() As Boolean
            If (Me.IsPowerManagementSupportedNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeQuotasDisabled() As Boolean
            If (Me.IsQuotasDisabledNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Sub ResetQuotasDisabled()
            curObj("QuotasDisabled") = Nothing
            If ((isEmbedded = false)  _
                        AndAlso (AutoCommitProp = true)) Then
                PrivateLateBoundObject.Put
            End If
        End Sub
        
        Private Function ShouldSerializeQuotasIncomplete() As Boolean
            If (Me.IsQuotasIncompleteNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeQuotasRebuilding() As Boolean
            If (Me.IsQuotasRebuildingNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeSize() As Boolean
            If (Me.IsSizeNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeStatusInfo() As Boolean
            If (Me.IsStatusInfoNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeSupportsDiskQuotas() As Boolean
            If (Me.IsSupportsDiskQuotasNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeSupportsFileBasedCompression() As Boolean
            If (Me.IsSupportsFileBasedCompressionNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Function ShouldSerializeVolumeDirty() As Boolean
            If (Me.IsVolumeDirtyNull = false) Then
                Return true
            End If
            Return false
        End Function
        
        Private Sub ResetVolumeName()
            curObj("VolumeName") = Nothing
            If ((isEmbedded = false)  _
                        AndAlso (AutoCommitProp = true)) Then
                PrivateLateBoundObject.Put
            End If
        End Sub
        
        <Browsable(true)>  _
        Public Overloads Sub CommitObject()
            If (isEmbedded = false) Then
                PrivateLateBoundObject.Put
            End If
        End Sub
        
        <Browsable(true)>  _
        Public Overloads Sub CommitObject(ByVal putOptions As System.Management.PutOptions)
            If (isEmbedded = false) Then
                PrivateLateBoundObject.Put(putOptions)
            End If
        End Sub
        
        Private Sub Initialize()
            AutoCommitProp = true
            isEmbedded = false
        End Sub
        
        Private Shared Function ConstructPath(ByVal keyDeviceID As String) As String
            Dim strPath As String = "ROOT\CIMV2:Win32_LogicalDisk"
            strPath = String.Concat(strPath, String.Concat(".DeviceID=", String.Concat("""", String.Concat(keyDeviceID, """"))))
            Return strPath
        End Function
        
        Private Sub InitializeObject(ByVal mgmtScope As System.Management.ManagementScope, ByVal path As System.Management.ManagementPath, ByVal getOptions As System.Management.ObjectGetOptions)
            Initialize
            If (Not (path) Is Nothing) Then
                If (CheckIfProperClass(mgmtScope, path, getOptions) <> true) Then
                    Throw New System.ArgumentException("Class name does not match.")
                End If
            End If
            PrivateLateBoundObject = New System.Management.ManagementObject(mgmtScope, path, getOptions)
            PrivateSystemProperties = New ManagementSystemProperties(PrivateLateBoundObject)
            curObj = PrivateLateBoundObject
        End Sub
        
        'Different overloads of GetInstances() help in enumerating instances of the WMI class.
        Public Overloads Shared Function GetInstances() As LogicalDiskCollection
            Return GetInstances(Nothing, Nothing, Nothing)
        End Function
        
        Public Overloads Shared Function GetInstances(ByVal condition As String) As LogicalDiskCollection
            Return GetInstances(Nothing, condition, Nothing)
        End Function
        
        Public Overloads Shared Function GetInstances(ByVal selectedProperties() As System.String ) As LogicalDiskCollection
            Return GetInstances(Nothing, Nothing, selectedProperties)
        End Function
        
        Public Overloads Shared Function GetInstances(ByVal condition As String, ByVal selectedProperties() As System.String ) As LogicalDiskCollection
            Return GetInstances(Nothing, condition, selectedProperties)
        End Function
        
        Public Overloads Shared Function GetInstances(ByVal mgmtScope As System.Management.ManagementScope, ByVal enumOptions As System.Management.EnumerationOptions) As LogicalDiskCollection
            If (mgmtScope Is Nothing) Then
                If (statMgmtScope Is Nothing) Then
                    mgmtScope = New System.Management.ManagementScope
                    mgmtScope.Path.NamespacePath = "root\CIMV2"
                Else
                    mgmtScope = statMgmtScope
                End If
            End If
            Dim pathObj As System.Management.ManagementPath = New System.Management.ManagementPath
            pathObj.ClassName = "Win32_LogicalDisk"
            pathObj.NamespacePath = "root\CIMV2"
            Dim clsObject As System.Management.ManagementClass = New System.Management.ManagementClass(mgmtScope, pathObj, Nothing)
            If (enumOptions Is Nothing) Then
                enumOptions = New System.Management.EnumerationOptions
                enumOptions.EnsureLocatable = true
            End If
            Return New LogicalDiskCollection(clsObject.GetInstances(enumOptions))
        End Function
        
        Public Overloads Shared Function GetInstances(ByVal mgmtScope As System.Management.ManagementScope, ByVal condition As String) As LogicalDiskCollection
            Return GetInstances(mgmtScope, condition, Nothing)
        End Function
        
        Public Overloads Shared Function GetInstances(ByVal mgmtScope As System.Management.ManagementScope, ByVal selectedProperties() As System.String ) As LogicalDiskCollection
            Return GetInstances(mgmtScope, Nothing, selectedProperties)
        End Function
        
        Public Overloads Shared Function GetInstances(ByVal mgmtScope As System.Management.ManagementScope, ByVal condition As String, ByVal selectedProperties() As System.String ) As LogicalDiskCollection
            If (mgmtScope Is Nothing) Then
                If (statMgmtScope Is Nothing) Then
                    mgmtScope = New System.Management.ManagementScope
                    mgmtScope.Path.NamespacePath = "root\CIMV2"
                Else
                    mgmtScope = statMgmtScope
                End If
            End If
            Dim ObjectSearcher As System.Management.ManagementObjectSearcher = New System.Management.ManagementObjectSearcher(mgmtScope, New SelectQuery("Win32_LogicalDisk", condition, selectedProperties))
            Dim enumOptions As System.Management.EnumerationOptions = New System.Management.EnumerationOptions
            enumOptions.EnsureLocatable = true
            ObjectSearcher.Options = enumOptions
            Return New LogicalDiskCollection(ObjectSearcher.Get)
        End Function
        
        <Browsable(true)>  _
        Public Shared Function CreateInstance() As LogicalDisk
            Dim mgmtScope As System.Management.ManagementScope = Nothing
            If (statMgmtScope Is Nothing) Then
                mgmtScope = New System.Management.ManagementScope
                mgmtScope.Path.NamespacePath = CreatedWmiNamespace
            Else
                mgmtScope = statMgmtScope
            End If
            Dim mgmtPath As System.Management.ManagementPath = New System.Management.ManagementPath(CreatedClassName)
            Dim tmpMgmtClass As System.Management.ManagementClass = New System.Management.ManagementClass(mgmtScope, mgmtPath, Nothing)
            Return New LogicalDisk(tmpMgmtClass.CreateInstance)
        End Function
        
        <Browsable(true)>  _
        Public Sub Delete()
            PrivateLateBoundObject.Delete
        End Sub
        
        Public Function Chkdsk(ByVal FixErrors As Boolean, ByVal ForceDismount As Boolean, ByVal OkToRunAtBootUp As Boolean, ByVal RecoverBadSectors As Boolean, ByVal SkipFolderCycle As Boolean, ByVal VigorousIndexCheck As Boolean) As UInteger
            If (isEmbedded = false) Then
                Dim inParams As System.Management.ManagementBaseObject = Nothing
                inParams = PrivateLateBoundObject.GetMethodParameters("Chkdsk")
                inParams("FixErrors") = CType(FixErrors,System.Boolean )
                inParams("ForceDismount") = CType(ForceDismount,System.Boolean )
                inParams("OkToRunAtBootUp") = CType(OkToRunAtBootUp,System.Boolean )
                inParams("RecoverBadSectors") = CType(RecoverBadSectors,System.Boolean )
                inParams("SkipFolderCycle") = CType(SkipFolderCycle,System.Boolean )
                inParams("VigorousIndexCheck") = CType(VigorousIndexCheck,System.Boolean )
                Dim outParams As System.Management.ManagementBaseObject = PrivateLateBoundObject.InvokeMethod("Chkdsk", inParams, Nothing)
                Return System.Convert.ToUInt32(outParams.Properties("ReturnValue").Value)
            Else
                Return System.Convert.ToUInt32(0)
            End If
        End Function
        
        Public Shared Function ExcludeFromAutochk(ByVal LogicalDisk() As String) As UInteger
            Dim IsMethodStatic As Boolean = true
            If (IsMethodStatic = true) Then
                Dim inParams As System.Management.ManagementBaseObject = Nothing
                Dim mgmtPath As System.Management.ManagementPath = New System.Management.ManagementPath(CreatedClassName)
                Dim classObj As System.Management.ManagementClass = New System.Management.ManagementClass(statMgmtScope, mgmtPath, Nothing)
                inParams = classObj.GetMethodParameters("ExcludeFromAutochk")
                inParams("LogicalDisk") = CType(LogicalDisk,String())
                Dim outParams As System.Management.ManagementBaseObject = classObj.InvokeMethod("ExcludeFromAutochk", inParams, Nothing)
                Return System.Convert.ToUInt32(outParams.Properties("ReturnValue").Value)
            Else
                Return System.Convert.ToUInt32(0)
            End If
        End Function
        
        Public Function Reset() As UInteger
            If (isEmbedded = false) Then
                Dim inParams As System.Management.ManagementBaseObject = Nothing
                Dim outParams As System.Management.ManagementBaseObject = PrivateLateBoundObject.InvokeMethod("Reset", inParams, Nothing)
                Return System.Convert.ToUInt32(outParams.Properties("ReturnValue").Value)
            Else
                Return System.Convert.ToUInt32(0)
            End If
        End Function
        
        Public Shared Function ScheduleAutoChk(ByVal LogicalDisk() As String) As UInteger
            Dim IsMethodStatic As Boolean = true
            If (IsMethodStatic = true) Then
                Dim inParams As System.Management.ManagementBaseObject = Nothing
                Dim mgmtPath As System.Management.ManagementPath = New System.Management.ManagementPath(CreatedClassName)
                Dim classObj As System.Management.ManagementClass = New System.Management.ManagementClass(statMgmtScope, mgmtPath, Nothing)
                inParams = classObj.GetMethodParameters("ScheduleAutoChk")
                inParams("LogicalDisk") = CType(LogicalDisk,String())
                Dim outParams As System.Management.ManagementBaseObject = classObj.InvokeMethod("ScheduleAutoChk", inParams, Nothing)
                Return System.Convert.ToUInt32(outParams.Properties("ReturnValue").Value)
            Else
                Return System.Convert.ToUInt32(0)
            End If
        End Function
        
        Public Function SetPowerState(ByVal PowerState As UShort, ByVal Time As Date) As UInteger
            If (isEmbedded = false) Then
                Dim inParams As System.Management.ManagementBaseObject = Nothing
                inParams = PrivateLateBoundObject.GetMethodParameters("SetPowerState")
                inParams("PowerState") = CType(PowerState,System.UInt16 )
                inParams("Time") = ToDmtfDateTime(CType(Time,Date))
                Dim outParams As System.Management.ManagementBaseObject = PrivateLateBoundObject.InvokeMethod("SetPowerState", inParams, Nothing)
                Return System.Convert.ToUInt32(outParams.Properties("ReturnValue").Value)
            Else
                Return System.Convert.ToUInt32(0)
            End If
        End Function
        
        Public Enum AccessValues
            
            Unknown0 = 0
            
            Readable = 1
            
            Writeable = 2
            
            Read_Write_Supported = 3
            
            Write_Once = 4
            
            NULL_ENUM_VALUE = 5
        End Enum
        
        Public Enum AvailabilityValues
            
            Other0 = 1
            
            Unknown0 = 2
            
            Running_Full_Power = 3
            
            Warning = 4
            
            In_Test = 5
            
            Not_Applicable = 6
            
            Power_Off = 7
            
            Off_Line = 8
            
            Off_Duty = 9
            
            Degraded = 10
            
            Not_Installed = 11
            
            Install_Error = 12
            
            Power_Save_Unknown = 13
            
            Power_Save_Low_Power_Mode = 14
            
            Power_Save_Standby = 15
            
            Power_Cycle = 16
            
            Power_Save_Warning = 17
            
            Paused = 18
            
            Not_Ready = 19
            
            Not_Configured = 20
            
            Quiesced = 21
            
            NULL_ENUM_VALUE = 0
        End Enum
        
        Public Enum ConfigManagerErrorCodeValues
            
            This_device_is_working_properly_ = 0
            
            This_device_is_not_configured_correctly_ = 1
            
            Windows_cannot_load_the_driver_for_this_device_ = 2
            
            The_driver_for_this_device_might_be_corrupted_or_your_system_may_be_running_low_on_memory_or_other_resources_ = 3
            
            This_device_is_not_working_properly_One_of_its_drivers_or_your_registry_might_be_corrupted_ = 4
            
            The_driver_for_this_device_needs_a_resource_that_Windows_cannot_manage_ = 5
            
            The_boot_configuration_for_this_device_conflicts_with_other_devices_ = 6
            
            Cannot_filter_ = 7
            
            The_driver_loader_for_the_device_is_missing_ = 8
            
            This_device_is_not_working_properly_because_the_controlling_firmware_is_reporting_the_resources_for_the_device_incorrectly_ = 9
            
            This_device_cannot_start_ = 10
            
            This_device_failed_ = 11
            
            This_device_cannot_find_enough_free_resources_that_it_can_use_ = 12
            
            Windows_cannot_verify_this_device_s_resources_ = 13
            
            This_device_cannot_work_properly_until_you_restart_your_computer_ = 14
            
            This_device_is_not_working_properly_because_there_is_probably_a_re_enumeration_problem_ = 15
            
            Windows_cannot_identify_all_the_resources_this_device_uses_ = 16
            
            This_device_is_asking_for_an_unknown_resource_type_ = 17
            
            Reinstall_the_drivers_for_this_device_ = 18
            
            Failure_using_the_VxD_loader_ = 19
            
            Your_registry_might_be_corrupted_ = 20
            
            System_failure_Try_changing_the_driver_for_this_device_If_that_does_not_work_see_your_hardware_documentation_Windows_is_removing_this_device_ = 21
            
            This_device_is_disabled_ = 22
            
            System_failure_Try_changing_the_driver_for_this_device_If_that_doesn_t_work_see_your_hardware_documentation_ = 23
            
            This_device_is_not_present_is_not_working_properly_or_does_not_have_all_its_drivers_installed_ = 24
            
            Windows_is_still_setting_up_this_device_ = 25
            
            Windows_is_still_setting_up_this_device_0 = 26
            
            This_device_does_not_have_valid_log_configuration_ = 27
            
            The_drivers_for_this_device_are_not_installed_ = 28
            
            This_device_is_disabled_because_the_firmware_of_the_device_did_not_give_it_the_required_resources_ = 29
            
            This_device_is_using_an_Interrupt_Request_IRQ_resource_that_another_device_is_using_ = 30
            
            This_device_is_not_working_properly_because_Windows_cannot_load_the_drivers_required_for_this_device_ = 31
            
            NULL_ENUM_VALUE = 32
        End Enum
        
        Public Enum DriveTypeValues
            
            Unknown0 = 0
            
            No_Root_Directory = 1
            
            Removable_Disk = 2
            
            Local_Disk = 3
            
            Network_Drive = 4
            
            Compact_Disc = 5
            
            RAM_Disk = 6
            
            NULL_ENUM_VALUE = 7
        End Enum
        
        Public Enum MediaTypeValues
            
            Format_is_unknown = 0
            
            Val_5_Inch_Floppy_Disk = 1
            
            Val_3_Inch_Floppy_Disk = 2
            
            Val_3_Inch_Floppy_Disk0 = 3
            
            Val_3_Inch_Floppy_Disk1 = 4
            
            Val_3_Inch_Floppy_Disk2 = 5
            
            Val_5_Inch_Floppy_Disk0 = 6
            
            Val_5_Inch_Floppy_Disk1 = 7
            
            Val_5_Inch_Floppy_Disk2 = 8
            
            Val_5_Inch_Floppy_Disk3 = 9
            
            Val_5_Inch_Floppy_Disk4 = 10
            
            Removable_media_other_than_floppy = 11
            
            Fixed_hard_disk_media = 12
            
            Val_3_Inch_Floppy_Disk3 = 13
            
            Val_3_Inch_Floppy_Disk4 = 14
            
            Val_5_Inch_Floppy_Disk5 = 15
            
            Val_5_Inch_Floppy_Disk6 = 16
            
            Val_3_Inch_Floppy_Disk5 = 17
            
            Val_3_Inch_Floppy_Disk6 = 18
            
            Val_5_Inch_Floppy_Disk7 = 19
            
            Val_3_Inch_Floppy_Disk7 = 20
            
            Val_3_Inch_Floppy_Disk8 = 21
            
            Val_8_Inch_Floppy_Disk = 22
            
            NULL_ENUM_VALUE = 23
        End Enum
        
        Public Enum PowerManagementCapabilitiesValues
            
            Unknown0 = 0
            
            Not_Supported = 1
            
            Disabled = 2
            
            Enabled = 3
            
            Power_Saving_Modes_Entered_Automatically = 4
            
            Power_State_Settable = 5
            
            Power_Cycling_Supported = 6
            
            Timed_Power_On_Supported = 7
            
            NULL_ENUM_VALUE = 8
        End Enum
        
        Public Enum StatusInfoValues
            
            Other0 = 1
            
            Unknown0 = 2
            
            Enabled = 3
            
            Disabled = 4
            
            Not_Applicable = 5
            
            NULL_ENUM_VALUE = 0
        End Enum
        
        'Enumerator implementation for enumerating instances of the class.
        Public Class LogicalDiskCollection
            Inherits Object
            Implements ICollection
            
            Private privColObj As ManagementObjectCollection
            
            Public Sub New(ByVal objCollection As ManagementObjectCollection)
                MyBase.New
                privColObj = objCollection
            End Sub
            
            Public Overridable ReadOnly Property Count() As Integer Implements System.Collections.ICollection.Count
                Get
                    Return privColObj.Count
                End Get
            End Property
            
            Public Overridable ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
                Get
                    Return privColObj.IsSynchronized
                End Get
            End Property
            
            Public Overridable ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
                Get
                    Return Me
                End Get
            End Property
            
            Public Overridable Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo
                privColObj.CopyTo(array, index)
                Dim nCtr As Integer
                nCtr = 0
                Do While (nCtr < array.Length)
                    array.SetValue(New LogicalDisk(CType(array.GetValue(nCtr),System.Management.ManagementObject)), nCtr)
                    nCtr = (nCtr + 1)
                Loop
            End Sub
            
            Public Overridable Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
                Return New LogicalDiskEnumerator(privColObj.GetEnumerator)
            End Function
            
            Public Class LogicalDiskEnumerator
                Inherits Object
                Implements System.Collections.IEnumerator
                
                Private privObjEnum As ManagementObjectCollection.ManagementObjectEnumerator
                
                Public Sub New(ByVal objEnum As ManagementObjectCollection.ManagementObjectEnumerator)
                    MyBase.New
                    privObjEnum = objEnum
                End Sub
                
                Public Overridable ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
                    Get
                        Return New LogicalDisk(CType(privObjEnum.Current,System.Management.ManagementObject))
                    End Get
                End Property
                
                Public Overridable Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
                    Return privObjEnum.MoveNext
                End Function
                
                Public Overridable Sub Reset() Implements System.Collections.IEnumerator.Reset
                    privObjEnum.Reset
                End Sub
            End Class
        End Class
        
        'TypeConverter to handle null values for ValueType properties
        Public Class WMIValueTypeConverter
            Inherits TypeConverter
            
            Private baseConverter As TypeConverter
            
            Private baseType As System.Type
            
            Public Sub New(ByVal inBaseType As System.Type)
                MyBase.New
                baseConverter = TypeDescriptor.GetConverter(inBaseType)
                baseType = inBaseType
            End Sub
            
            Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal srcType As System.Type) As Boolean
                Return baseConverter.CanConvertFrom(context, srcType)
            End Function
            
            Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
                Return baseConverter.CanConvertTo(context, destinationType)
            End Function
            
            Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
                Return baseConverter.ConvertFrom(context, culture, value)
            End Function
            
            Public Overloads Overrides Function CreateInstance(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal dictionary As System.Collections.IDictionary) As Object
                Return baseConverter.CreateInstance(context, dictionary)
            End Function
            
            Public Overloads Overrides Function GetCreateInstanceSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                Return baseConverter.GetCreateInstanceSupported(context)
            End Function
            
            Public Overloads Overrides Function GetProperties(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As Object, ByVal attributeVar() As System.Attribute) As PropertyDescriptorCollection
                Return baseConverter.GetProperties(context, value, attributeVar)
            End Function
            
            Public Overloads Overrides Function GetPropertiesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                Return baseConverter.GetPropertiesSupported(context)
            End Function
            
            Public Overloads Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
                Return baseConverter.GetStandardValues(context)
            End Function
            
            Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                Return baseConverter.GetStandardValuesExclusive(context)
            End Function
            
            Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                Return baseConverter.GetStandardValuesSupported(context)
            End Function
            
            Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
                If (baseType.BaseType Is GetType(System.[Enum])) Then
                    If (value.GetType Is destinationType) Then
                        Return value
                    End If
                    If (((value = Nothing)  _
                                AndAlso (Not (context) Is Nothing))  _
                                AndAlso (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) = false)) Then
                        Return  "NULL_ENUM_VALUE" 
                    End If
                    Return baseConverter.ConvertTo(context, culture, value, destinationType)
                End If
                If ((baseType Is GetType(Boolean))  _
                            AndAlso (baseType.BaseType Is GetType(System.ValueType))) Then
                    If (((value = Nothing)  _
                                AndAlso (Not (context) Is Nothing))  _
                                AndAlso (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) = false)) Then
                        Return ""
                    End If
                    Return baseConverter.ConvertTo(context, culture, value, destinationType)
                End If
                If ((Not (context) Is Nothing)  _
                            AndAlso (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) = false)) Then
                    Return ""
                End If
                Return baseConverter.ConvertTo(context, culture, value, destinationType)
            End Function
        End Class
        
        'Embedded class to represent WMI system Properties.
        <TypeConverter(GetType(System.ComponentModel.ExpandableObjectConverter))>  _
        Public Class ManagementSystemProperties
            
            Private PrivateLateBoundObject As System.Management.ManagementBaseObject
            
            Public Sub New(ByVal ManagedObject As System.Management.ManagementBaseObject)
                MyBase.New
                PrivateLateBoundObject = ManagedObject
            End Sub
            
            <Browsable(true)>  _
            Public ReadOnly Property GENUS() As Integer
                Get
                    Return CType(PrivateLateBoundObject("__GENUS"),Integer)
                End Get
            End Property
            
            <Browsable(true)>  _
            Public ReadOnly Property [CLASS]() As String
                Get
                    Return CType(PrivateLateBoundObject("__CLASS"),String)
                End Get
            End Property
            
            <Browsable(true)>  _
            Public ReadOnly Property SUPERCLASS() As String
                Get
                    Return CType(PrivateLateBoundObject("__SUPERCLASS"),String)
                End Get
            End Property
            
            <Browsable(true)>  _
            Public ReadOnly Property DYNASTY() As String
                Get
                    Return CType(PrivateLateBoundObject("__DYNASTY"),String)
                End Get
            End Property
            
            <Browsable(true)>  _
            Public ReadOnly Property RELPATH() As String
                Get
                    Return CType(PrivateLateBoundObject("__RELPATH"),String)
                End Get
            End Property
            
            <Browsable(true)>  _
            Public ReadOnly Property PROPERTY_COUNT() As Integer
                Get
                    Return CType(PrivateLateBoundObject("__PROPERTY_COUNT"),Integer)
                End Get
            End Property
            
            <Browsable(true)>  _
            Public ReadOnly Property DERIVATION() As String()
                Get
                    Return CType(PrivateLateBoundObject("__DERIVATION"),String())
                End Get
            End Property
            
            <Browsable(true)>  _
            Public ReadOnly Property SERVER() As String
                Get
                    Return CType(PrivateLateBoundObject("__SERVER"),String)
                End Get
            End Property
            
            <Browsable(true)>  _
            Public ReadOnly Property [NAMESPACE]() As String
                Get
                    Return CType(PrivateLateBoundObject("__NAMESPACE"),String)
                End Get
            End Property
            
            <Browsable(true)>  _
            Public ReadOnly Property PATH() As String
                Get
                    Return CType(PrivateLateBoundObject("__PATH"),String)
                End Get
            End Property
        End Class
    End Class
End Namespace
