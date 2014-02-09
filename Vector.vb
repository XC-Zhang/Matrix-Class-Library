''' <summary>
''' 处理与向量的相关操作的类
''' </summary>
''' <remarks></remarks>
Public Class Vector

    Private _elem As Double()

    ''' <summary>
    ''' 使用指定的维数初始化一个零向量
    ''' </summary>
    ''' <param name="dimensions">新的Vector实例的维数</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal dimensions As UInteger)

        ReDim _elem(dimensions - 1)

    End Sub

    ''' <summary>
    ''' 使用指定元素初始化Vector类的新实例
    ''' </summary>
    ''' <param name="vector">新Vector实例的元素数组</param>
    ''' <remarks></remarks>
    Public Sub New(ByRef vector As Double())

        If vector Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector")

        Else

            _elem = vector.Clone()

        End If

    End Sub

    ''' <summary>
    ''' 比较此实例与另一个向量是否相等
    ''' </summary>
    ''' <param name="vector">要与之比较的向量</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal vector As Object) As Boolean

        If vector Is Nothing OrElse [GetType]().Equals(vector.GetType()) Then

            Return False

        End If

        Dim v As Vector = CType(vector, Vector)

        If _elem.Length <> v._elem.Length Then

            Return False

        End If

        For i As Integer = 0 To _elem.Length - 1

            If _elem(i) <> v._elem(i) Then

                Return False

            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 将此实例转换为字符串
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String

        Dim s As String = "("

        For i As Integer = 0 To _elem.Length - 1

            s = s & _elem(i).ToString("0.0000") & " "

        Next

        s = s.Remove(s.Length - 1) & " "

        Return s

    End Function

    ''' <summary>
    ''' 获取一个整数，它表示该向量的维数
    ''' </summary>
    ''' <value></value>
    ''' <returns>一个整数，它表示该向量的维数</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Dimensions As Integer
        Get

            Return _elem.Length

        End Get
    End Property

    ''' <summary>
    ''' 获取一个双精度浮点值数组，它表示该向量的元素
    ''' </summary>
    ''' <value></value>
    ''' <returns>一个浮点值数组，它表示该向量的元素</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Elements As Double()
        Get

            Return _elem

        End Get
    End Property

    ''' <summary>
    ''' 获取一个值，该值指示此向量是否是零向量
    ''' </summary>
    ''' <value></value>
    ''' <returns>如果该向量是零向量，则该属性为true，否则为false</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsNullVector As Boolean
        Get

            For i As Integer = 0 To _elem.Length - 1

                If _elem(i) <> 0 Then

                    Return False

                End If

            Next

            Return True

        End Get
    End Property

    ''' <summary>
    ''' 获取一个值，该值指示此向量是否是单位向量
    ''' </summary>
    ''' <value></value>
    ''' <returns>如果该向量是单位向量，则该属性为true，否则为false</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsUnitVector As Boolean
        Get

            Return (Length = 1)

        End Get
    End Property

    ''' <summary>
    ''' 获取此实例中的某一维的分量
    ''' </summary>
    ''' <param name="index">要获取的分量在向量中的位置，从1开始</param>
    ''' <value></value>
    ''' <returns>一个双精度浮点值，表示指定维的分量</returns>
    ''' <remarks></remarks>
    Default Public ReadOnly Property Item(ByVal index As Integer) As Double
        Get

            Return _elem(index - 1)

        End Get
    End Property

    ''' <summary>
    ''' 获取一个双精度浮点值，该值指示此向量的模（长度）
    ''' </summary>
    ''' <value></value>
    ''' <returns>一个双精度浮点值，表示模</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Length As Double
        Get

            Dim s As Double

            For i As Integer = 0 To _elem.Length - 1

                s = s + _elem(i) ^ 2

            Next

            Dim l As Double = s ^ (1 / 2)

            Return l

        End Get
    End Property

    ''' <summary>
    ''' 创建此向量的一个精确副本
    ''' </summary>
    ''' <returns>此方法创建的Vector对象</returns>
    ''' <remarks></remarks>
    Public Function Clone() As Vector

        Dim v As New Vector(_elem.Length)

        For i As Integer = 0 To _elem.Length - 1

            v._elem(i) = _elem(i)

        Next

        Return v

    End Function

    ''' <summary>
    ''' 计算此实例与参数中的向量的和
    ''' </summary>
    ''' <param name="vector">与此实例进行相加的向量</param>
    ''' <returns>一个向量，表示和</returns>
    ''' <remarks></remarks>
    Public Function Add(ByRef vector As Vector) As Vector

        If vector Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector")

        End If

        If _elem.Length <> vector._elem.Length Then

            Throw New ArgumentException("向量的维数不相等", "vector")

        End If

        Dim v As New Vector(_elem.Length)

        For i As Integer = 0 To _elem.Length - 1

            v._elem(i) = _elem(i) + vector._elem(i)

        Next

        Return v

    End Function

    ''' <summary>
    ''' 计算此实例与参数中的向量的差
    ''' </summary>
    ''' <param name="vector">与此实例进行相减的向量</param>
    ''' <returns>一个向量，表示差</returns>
    ''' <remarks></remarks>
    Public Function Subtract(ByRef vector As Vector) As Vector

        If vector Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector")

        End If

        If _elem.Length <> vector._elem.Length Then

            Throw New ArgumentException("向量的维数不相等", "vector")

        End If

        Dim v As New Vector(_elem.Length)

        For i As Integer = 0 To _elem.Length - 1

            v._elem(i) = _elem(i) - vector._elem(i)

        Next

        Return v

    End Function

    ''' <summary>
    ''' 计算此实例与参数中的数量的积
    ''' </summary>
    ''' <param name="scalar">与此实例相乘的数</param>
    ''' <returns>一个向量，表示乘积</returns>
    ''' <remarks></remarks>
    Public Function ScalarMultiple(ByVal scalar As Double) As Vector

        Dim v As New Vector(_elem.Length)

        For i As Integer = 0 To _elem.Length - 1

            v._elem(i) = scalar * _elem(i)

        Next

        Return v

    End Function

    ''' <summary>
    ''' 计算此实例与参数中的向量的数量积（内积，点积）
    ''' </summary>
    ''' <param name="vector">与此实例进行点乘的向量</param>
    ''' <returns>一个双精度浮点值，表示数量积</returns>
    ''' <remarks></remarks>
    Public Function DotProduct(ByRef vector As Vector) As Double

        If vector Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector")

        End If

        If _elem.Length <> vector._elem.Length Then

            Throw New ArgumentException("向量的维数不相等", "vector")

        End If

        Dim p As Double

        For i As Integer = 0 To _elem.Length - 1

            p = p + _elem(i) * vector._elem(i)

        Next

        Return p

    End Function

    ''' <summary>
    ''' 计算此实例与参数中的向量的向量积（外积，叉积）
    ''' </summary>
    ''' <param name="vector">与此实例进行叉乘的向量</param>
    ''' <returns>一个向量，表示向量积</returns>
    ''' <remarks></remarks>
    Public Function CrossProduct(ByRef vector As Vector) As Vector

        If vector Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector")

        End If

        If _elem.Length <> vector._elem.Length Then

            Throw New ArgumentException("向量的维数不相等", "vector")

        End If

        If (_elem.Length <> 3) AndAlso (_elem.Length <> 7) Then

            Throw New ArgumentException("向量的维数不满足向量积的条件", "vector")

        End If

        Dim v As New Vector(_elem.Length)

        v._elem(0) = _elem(1) * vector._elem(2) - _elem(2) * vector._elem(1)

        v._elem(1) = _elem(2) * vector._elem(0) - _elem(0) * vector._elem(2)

        v._elem(2) = _elem(0) * vector._elem(1) - _elem(1) * vector._elem(0)

        Return v

    End Function

    ''' <summary>
    ''' 将此实例标准化，使其成为单位向量
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Normalize()

        If IsNullVector Then

            Throw New Exception("无法将零向量标准化")

        End If

        Dim l As Double = Length

        For i As Integer = 0 To _elem.Length - 1

            _elem(i) = _elem(i) / l

        Next

    End Sub

    ''' <summary>
    ''' 返回向量元素中的最大值
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Max() As Double
        Dim m As Double = _elem(0)
        For i As Integer = 1 To _elem.GetLength(0) - 1
            If m < _elem(i) Then
                m = _elem(i)
            End If
        Next
        Return m
    End Function

    ''' <summary>
    ''' 返回向量元素中的最小值
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Min() As Double
        Dim m As Double = _elem(0)
        For i As Integer = 1 To _elem.GetLength(0) - 1
            If m > _elem(i) Then
                m = _elem(i)
            End If
        Next
        Return m
    End Function

    ''' <summary>
    ''' 返回向量元素中绝对值的最大值
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AbsMax() As Double
        Dim m As Double = Math.Abs(_elem(0))
        For i As Integer = 1 To _elem.GetLength(0) - 1
            If m < Math.Abs(_elem(i)) Then
                m = Math.Abs(_elem(i))
            End If
        Next
        Return m
    End Function

    ''' <summary>
    ''' 返回向量元素中绝对值的最小值
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AbsMin() As Double
        Dim m As Double = Math.Abs(_elem(0))
        For i As Integer = 1 To _elem.GetLength(0) - 1
            If m > Math.Abs(_elem(i)) Then
                m = Math.Abs(_elem(i))
            End If
        Next
        Return m
    End Function

    Public Shared Operator +(ByVal vector1 As Vector, ByVal vector2 As Vector) As Vector

        If vector1 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector1")

        End If

        If vector2 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector2")

        End If

        Return vector1.Add(vector2)

    End Operator

    Public Shared Operator -(ByVal vector As Vector) As Vector

        If vector Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector")

        End If

        Dim v As New Vector(vector._elem.Length)

        For i As Integer = 0 To vector._elem.Length

            v._elem(i) = -vector._elem(i)

        Next

        Return v

    End Operator

    Public Shared Operator -(ByVal vector1 As Vector, ByVal vector2 As Vector) As Vector

        If vector1 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector1")

        End If

        If vector2 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector2")

        End If

        Return vector1.Subtract(vector2)

    End Operator

    Public Shared Operator *(ByVal scalar As Double, ByVal vector As Vector) As Vector

        If vector Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector")

        End If

        Return vector.ScalarMultiple(scalar)

    End Operator

    Public Shared Operator *(ByVal vector As Vector, ByVal scalar As Double) As Vector

        If vector Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector")

        End If

        Return vector.ScalarMultiple(scalar)

    End Operator

    Public Shared Operator =(ByVal vector1 As Vector, ByVal vector2 As Vector) As Boolean

        If vector1 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector1")

        End If

        If vector2 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector2")

        End If

        Return vector1.Equals(vector2)

    End Operator

    Public Shared Operator <>(ByVal vector1 As Vector, ByVal vector2 As Vector) As Boolean

        If vector1 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector1")

        End If

        If vector2 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector2")

        End If

        Return Not vector1.Equals(vector2)

    End Operator

    Public Shared Widening Operator CType(ByVal vector As Vector) As Matrix

        If vector Is Nothing Then

            Return Nothing

        End If

        Dim e(0, vector._elem.Length - 1) As Double

        For i As Integer = 0 To vector._elem.Length - 1

            e(0, i) = vector._elem(i)

        Next

        Return New Matrix(e)

    End Operator

    Public Shared Narrowing Operator CType(ByVal vector As Matrix) As Vector

        If vector Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "vector")

        End If

        If vector.RowCounts = 1 Then

            Return vector.Rows.Item(0)

        ElseIf vector.ColumnCounts = 1 Then

            Return vector.Columns(1)

        Else

            Throw New ArgumentException("无法将矩阵转换为向量", "vector")

        End If

    End Operator

End Class
