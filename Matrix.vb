''' <summary>
''' 处理与矩阵相关的操作的类
''' </summary>
''' <remarks></remarks>
Public Class Matrix

    Protected _elem As Double(,)

    Protected Sub New()

    End Sub

    ''' <summary>
    ''' 以指定的行数和列数创建一个矩阵
    ''' </summary>
    ''' <param name="rows">矩阵的行数</param>
    ''' <param name="columns">矩阵的列数</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal rows As Integer, ByVal columns As Integer)

        ReDim _elem(rows - 1, columns - 1)

    End Sub

    ''' <summary>
    ''' 用二维数组创建一个矩阵
    ''' </summary>
    ''' <param name="matrix"></param>
    ''' <remarks></remarks>
    Public Sub New(ByRef matrix As Double(,))

        If matrix Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix")

        Else

            _elem = matrix.Clone()

        End If

    End Sub

    ''' <summary>
    ''' 测试指定的对象是否是Matrix类，以及是否与此矩阵相等
    ''' </summary>
    ''' <param name="matrix"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal matrix As Object) As Boolean

        If matrix Is Nothing OrElse [GetType]().Equals(matrix.GetType()) Then

            Return False

        End If

        Dim m As Matrix = CType(matrix, Matrix)

        If (_elem.GetLength(0) <> m._elem.GetLength(0)) OrElse (_elem.GetLength(1) <> m._elem.GetLength(1)) Then

            Return False

        End If

        For i As Integer = 0 To _elem.GetLength(0) - 1

            For j As Integer = 0 To _elem.GetLength(1) - 1

                If _elem(i, j) <> m._elem(i, j) Then

                    Return False

                End If

            Next

        Next

        Return True

    End Function

    ''' <summary>
    ''' 获取一个浮点值数组，它表示该矩阵的元素
    ''' </summary>
    ''' <value></value>
    ''' <returns>一个浮点值数组，它表示该矩阵的元素</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Entries As Double(,)
        Get

            Return _elem

        End Get
    End Property

    ''' <summary>
    ''' 获取或设置矩阵中某个位置的值
    ''' </summary>
    ''' <param name="row">要设置的值的行号，从1开始</param>
    ''' <param name="column">要设置的值的列号，从1开始</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Public Property Element(ByVal row As Integer, ByVal column As Integer) As Double
        Get
            If (row <= 0) Or (row > _elem.GetLength(0)) Or (column <= 0) Or (column > _elem.GetLength(1)) Then
                Throw New ArgumentException("行号或列号超出矩阵范围")
            End If
            Return _elem(row - 1, column - 1)
        End Get
        Set(ByVal value As Double)
            If (row <= 0) Or (row > _elem.GetLength(0)) Or (column <= 0) Or (column > _elem.GetLength(1)) Then
                Throw New ArgumentException("行号或列号超出矩阵范围")
            End If
            _elem(row - 1, column - 1) = value
        End Set
    End Property

    ''' <summary>
    ''' 获取一个值，该值指示此矩阵是否是单位矩阵
    ''' </summary>
    ''' <value></value>
    ''' <returns>如果该矩阵是单位矩阵，则该属性为True，否则为False</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsIdentity As Boolean
        Get

            For i As Integer = 0 To _elem.GetLength(0) - 1

                For j As Integer = 0 To _elem.GetLength(1) - 1

                    If (i <> j) AndAlso (_elem(i, j) <> 0) OrElse (i = j) AndAlso (_elem(i, j) <> 1) Then

                        Return False

                    End If

                Next

            Next

            Return True

        End Get
    End Property

    ''' <summary>
    ''' 表示矩阵中的行向量的集合
    ''' </summary>
    ''' <value></value>
    ''' <returns>一个MatrixRowCollection的实例，表示矩阵中的各个行向量</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Rows As MatrixRowCollection
        Get

            Return New MatrixRowCollection(Me)

        End Get
    End Property

    ''' <summary>
    ''' 获取一个整型数值，该值表示此矩阵的行数
    ''' </summary>
    ''' <value></value>
    ''' <returns>一个整型数值，表示此矩阵的行数</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RowCounts As Integer
        Get

            Return _elem.GetLength(0)

        End Get
    End Property

    ''' <summary>
    ''' 表示矩阵中的列向量的集合
    ''' </summary>
    ''' <value></value>
    ''' <returns>一个MatrixColumnCollection的实例，表示矩阵中的各个列向量</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Columns As MatrixColumnCollection
        Get

            Return New MatrixColumnCollection(Me)

        End Get
    End Property

    ''' <summary>
    ''' 获取一个整型数值，该值表示此矩阵的列数
    ''' </summary>
    ''' <value></value>
    ''' <returns>一个整型数值，表示此矩阵的列数</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ColumnCounts As Integer
        Get

            Return _elem.GetLength(1)

        End Get
    End Property

    ''' <summary>
    ''' 返回一个指定大小的单位矩阵
    ''' </summary>
    ''' <param name="dimension">矩阵的大小</param>
    ''' <returns>一个Matrix对象，它的对角线上的元素是1</returns>
    ''' <remarks></remarks>
    Public Shared Function Identity(ByVal dimension As Integer) As Matrix

        Dim m As New Matrix(dimension, dimension)

        For i As Integer = 0 To dimension - 1

            m._elem(i, i) = 1

        Next

        Return m

    End Function

    ''' <summary>
    ''' 创建此Matrix的一个精确副本
    ''' </summary>
    ''' <returns>此方法创建的Matrix实例</returns>
    ''' <remarks></remarks>
    Public Function Clone() As Matrix

        Return New Matrix(_elem)

    End Function

    ''' <summary>
    ''' 计算此实例与参数中的矩阵的和
    ''' </summary>
    ''' <param name="matrix">要与此矩阵相加的矩阵</param>
    ''' <returns>一个矩阵，表示和</returns>
    ''' <remarks></remarks>
    Public Function Add(ByRef matrix As Matrix) As Matrix

        If matrix Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix")

        End If

        If (_elem.GetLength(0) <> matrix._elem.GetLength(0)) OrElse (_elem.GetLength(1) <> matrix._elem.GetLength(1)) Then

            Throw New ArgumentException("矩阵大小不一致", "matrix")

        End If

        Dim m As New Matrix(_elem.GetLength(0), _elem.GetLength(1))

        For i As Integer = 0 To _elem.GetLength(0) - 1

            For j As Integer = 0 To _elem.GetLength(1) - 1

                m._elem(i, j) = _elem(i, j) + matrix._elem(i, j)

            Next

        Next

        Return m

    End Function

    ''' <summary>
    ''' 计算此实例与参数中的矩阵的差
    ''' </summary>
    ''' <param name="matrix">要与此矩阵相减的矩阵</param>
    ''' <returns>一个矩阵，表示差</returns>
    ''' <remarks></remarks>
    Public Function Subtract(ByRef matrix As Matrix) As Matrix

        If matrix Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix")

        End If

        If (_elem.GetLength(0) <> matrix._elem.GetLength(0)) OrElse (_elem.GetLength(1) <> matrix._elem.GetLength(1)) Then

            Throw New ArgumentException("矩阵大小不一致", "matrix")

        End If

        Dim m As New Matrix(_elem.GetLength(0), _elem.GetLength(1))

        For i As Integer = 0 To _elem.GetLength(0) - 1

            For j As Integer = 0 To _elem.GetLength(1) - 1

                m._elem(i, j) = _elem(i, j) - matrix._elem(i, j)

            Next

        Next

        Return m

    End Function

    ''' <summary>
    ''' 计算此实例与参数中的数的积
    ''' </summary>
    ''' <param name="scalar">与此矩阵相乘的数</param>
    ''' <returns>一个矩阵，表示乘积</returns>
    ''' <remarks></remarks>
    Public Function ScalarMultiply(ByVal scalar As Double) As Matrix

        Dim m As New Matrix(_elem.GetLength(0), _elem.GetLength(1))

        For i As Integer = 0 To _elem.GetLength(0) - 1

            For j As Integer = 0 To _elem.GetLength(1) - 1

                m._elem(i, j) = scalar * _elem(i, j)

            Next

        Next

        Return m

    End Function

    ''' <summary>
    ''' 计算此实例左乘参数中的矩阵的乘积
    ''' </summary>
    ''' <param name="matrix">要被此矩阵左乘的矩阵</param>
    ''' <returns>一个矩阵，表示乘积</returns>
    ''' <remarks></remarks>
    Public Function Multiply(ByRef matrix As Matrix) As Matrix

        If matrix Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix")

        End If

        If _elem.GetLength(1) <> matrix._elem.GetLength(0) Then

            Throw New ArgumentException("矩阵大小不匹配", "matrix")

        End If

        Dim m As New Matrix(_elem.GetLength(0), matrix._elem.GetLength(1))

        For i As Integer = 0 To _elem.GetLength(0) - 1

            For j As Integer = 0 To matrix._elem.GetLength(1) - 1

                m._elem(i, j) = Rows(i + 1).DotProduct(matrix.Columns(j + 1))

            Next

        Next

        Return m

    End Function

    ''' <summary>
    ''' 计算转置矩阵
    ''' </summary>
    ''' <returns>转置矩阵</returns>
    ''' <remarks></remarks>
    Public Function Transpose() As Matrix

        Dim rows As Integer = _elem.GetLength(0)
        Dim columns As Integer = _elem.GetLength(1)
        Dim m As Matrix = New Matrix()

        ReDim m._elem(columns - 1, rows - 1)

        For i As Integer = 0 To rows - 1

            For j As Integer = 0 To columns - 1

                m._elem(j, i) = _elem(i, j)

            Next

        Next

        Return m

    End Function

    ''' <summary>
    ''' 计算此实例的逆矩阵
    ''' </summary>
    ''' <returns>一个矩阵，它是这个矩阵的逆</returns>
    ''' <remarks></remarks>
    Public Function Invert() As Matrix

        If _elem.GetLength(0) <> _elem.GetLength(1) Then

            Throw New InvalidOperationException("该矩阵不是方阵")

        End If

        Dim A As Matrix = Clone()

        Dim B As Matrix = Identity(_elem.GetLength(0))

        For i As Integer = 0 To _elem.GetLength(0) - 1

            Dim j As Integer = i + 1

            If A._elem(i, i) = 0 Then

                Do While (_elem(j, i) = 0)

                    j = j + 1

                    If j = _elem.GetLength(0) Then

                        Throw New InvalidOperationException("该矩阵是奇异矩阵")

                    End If

                Loop

                A.RowSwitch(i, j)
                B.RowSwitch(i, j)

            End If

            Dim m As Double

            m = A._elem(i, i)
            A.RowMultiply(1 / m, i)
            B.RowMultiply(1 / m, i)

            For j = 0 To _elem.GetLength(0) - 1

                If j <> i Then
                    m = A._elem(j, i)
                    A.RowAdd(-m, j, i)
                    B.RowAdd(-m, j, i)
                End If

            Next

        Next

        Return B

    End Function

    ''' <summary>
    ''' 改变矩阵的大小，并保留原来已有的元素
    ''' </summary>
    ''' <param name="row">新的行数</param>
    ''' <param name="column">新的列数</param>
    ''' <remarks></remarks>
    Public Sub [ReDim](ByVal row As Integer, ByVal column As Integer)
        If row = _elem.GetLength(0) Then

            ReDim Preserve _elem(row, column)

        Else
            Dim newelem(row - 1, column - 1) As Double

            For i As Integer = 0 To Math.Min(_elem.GetLength(0), row) - 1

                For j As Integer = 0 To Math.Min(_elem.GetLength(1), column) - 1

                    newelem(i, j) = _elem(i, j)

                Next

            Next

            _elem = newelem
        End If
    End Sub

    ''' <summary>
    ''' 返回矩阵的格式的字符串
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Dim L(_elem.GetLength(1) - 1) As Integer
        Dim R(_elem.GetLength(1) - 1) As Integer
        For i As Integer = 0 To _elem.GetLength(1) - 1
            Dim max = Columns(i + 1).AbsMax
            Dim min = Columns(i + 1).AbsMin
            If max >= 10 Then
                L(i) = Math.Log10(max) + 1
            Else
                L(i) = 1
            End If
            If Columns(i + 1).Min < 0.0 Then
                L(i) = L(i) + 1
            End If
            R(i) = 10
        Next
        Dim sAll, s, formatted As String
        Dim len As Integer
        sAll = String.Empty
        For i As Integer = 0 To _elem.GetLength(0) - 1
            For j As Integer = 0 To _elem.GetLength(1) - 1
                formatted = StrDup(L(j), "#"c) + "0." + StrDup(R(j), "0"c)
                s = _elem(i, j).ToString(formatted)
                len = s.Length
                sAll = sAll + Space(L(j) + R(j) + 1 - len) + s + Space(1)
            Next
            sAll = sAll + vbCrLf
        Next
        Return sAll
    End Function

    ''' <summary>
    ''' 进行行交换操作，将矩阵的第i行与第j行交换
    ''' </summary>
    ''' <param name="i"></param>
    ''' <param name="j"></param>
    ''' <remarks></remarks>
    Private Sub RowSwitch(ByVal i As Integer, ByVal j As Integer)

        Dim t As Double

        For k As Integer = 0 To _elem.GetLength(1) - 1
            t = _elem(i, k)
            _elem(i, k) = _elem(j, k)
            _elem(j, k) = t
        Next

    End Sub

    ''' <summary>
    ''' 进行行乘法操作，将矩阵的第i行乘以m
    ''' </summary>
    ''' <param name="m"></param>
    ''' <param name="i"></param>
    ''' <remarks></remarks>
    Private Sub RowMultiply(ByVal m As Double, ByVal i As Integer)

        For k As Integer = 0 To _elem.GetLength(1) - 1
            _elem(i, k) *= m
        Next

    End Sub

    ''' <summary>
    ''' 进行行相加操作，将矩阵的第j行乘以m加到第i行上
    ''' </summary>
    ''' <param name="m"></param>
    ''' <param name="i"></param>
    ''' <param name="j"></param>
    ''' <remarks></remarks>
    Private Sub RowAdd(ByVal m As Double, ByVal i As Integer, ByVal j As Integer)

        For k As Integer = 0 To _elem.GetLength(1) - 1
            _elem(i, k) += _elem(j, k) * m
        Next

    End Sub

    ''' <summary>
    ''' 表示Matrix对象中行的集合
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MatrixRowCollection
        Implements IEnumerable

        Private _matrix As Matrix

        Friend Sub New(ByRef matrix As Matrix)

            _matrix = matrix

        End Sub

        ''' <summary>
        ''' 返回一个整数，它表示此实例对应的矩阵的行数
        ''' </summary>
        ''' <value></value>
        ''' <returns>一个整数，表示集合中元素的个数</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Count As Integer
            Get

                Return _matrix._elem.GetLength(0)

            End Get
        End Property

        ''' <summary>
        ''' 按行号返回一个行向量
        ''' </summary>
        ''' <param name="index">行号，从1开始</param>
        ''' <value></value>
        ''' <returns>一个向量，表示矩阵中指定行号的行向量</returns>
        ''' <remarks></remarks>
        Default Public ReadOnly Property Item(ByVal index As Integer) As Vector
            Get

                If (index <= 0) OrElse (index > _matrix._elem.GetLength(0)) Then

                    Throw New ArgumentException("行的序号超出矩阵范围", "index")

                End If

                Dim e(_matrix._elem.GetLength(1) - 1) As Double

                For i As Integer = 0 To _matrix._elem.GetLength(1) - 1

                    e(i) = _matrix._elem(index - 1, i)

                Next

                Return New Vector(e)

            End Get
        End Property

        ''' <summary>
        ''' 获取此实例对应的Matrix类的实例
        ''' </summary>
        ''' <value></value>
        ''' <returns>一个Matrix类的实例</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Matrix As Matrix
            Get

                Return _matrix

            End Get
        End Property

        ''' <summary>
        ''' 在指定的行之前插入指定的行向量
        ''' </summary>
        ''' <param name="row">要插入矩阵的行向量，行向量的列数必须与被修改的矩阵一致</param>
        ''' <param name="index">要将行向量插入的行号，从1开始</param>
        ''' <remarks></remarks>
        Public Sub InsertAt(ByRef row As Vector, ByVal index As Integer)

            If row Is Nothing Then

                Throw New ArgumentException("未将对象引用设置到实例", "row")

            End If

            Dim RowCounts As Integer = _matrix._elem.GetLength(0)

            If (index <= 0) Or (index > RowCounts + 1) Then

                Throw New ArgumentException("行号超出矩阵范围", "index")

            End If

            Dim ColumnCounts As Integer = _matrix._elem.GetLength(1)

            If row.Dimensions <> ColumnCounts Then

                Throw New ArgumentException("行向量的列数与矩阵不一致", "row")

            End If

            ReDim Preserve _matrix._elem(RowCounts, ColumnCounts - 1)

            For i As Integer = RowCounts To index Step -1

                For j As Integer = 0 To ColumnCounts - 1

                    _matrix._elem(i, j) = _matrix._elem(i - 1, j)

                Next

            Next

            For j As Integer = 0 To ColumnCounts - 1

                _matrix._elem(index - 1, j) = row(j + 1)

            Next

        End Sub

        ''' <summary>
        ''' 从矩阵中删除指定的行
        ''' </summary>
        ''' <param name="index">要删除的行号</param>
        ''' <remarks>如果矩阵只有一行，则此方法没有任何效果，也不会引发异常</remarks>
        Public Sub RemoveAt(ByVal index As Integer)

            If (index <= 0) OrElse (index > _matrix._elem.GetLength(0)) Then

                Throw New ArgumentException("行号超出矩阵范围", "index")

            End If

            For i As Integer = index - 1 To _matrix._elem.GetLength(0) - 1

                For j As Integer = 0 To _matrix._elem.GetLength(1) - 1

                    _matrix._elem(i, j) = _matrix._elem(i + 1, j)

                Next

            Next

        End Sub

        ''' <summary>
        ''' 返回一个循环访问集合的枚举器
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator

            Return New MatrixRowEnum(_matrix)

        End Function

        Public Class MatrixRowEnum
            Implements IEnumerator

            Private _matrix As Matrix

            Private position As Integer = -1

            Public Sub New(ByRef matrix As Matrix)

                _matrix = matrix

            End Sub

            Public ReadOnly Property Current As Object Implements System.Collections.IEnumerator.Current
                Get

                    Try

                        Return _matrix.Rows(position + 1)

                    Catch ex As Exception

                        Throw New InvalidOperationException()

                    End Try

                End Get
            End Property

            Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext

                position = position + 1

                Return (position < _matrix._elem.GetLength(0))

            End Function

            Public Sub Reset() Implements System.Collections.IEnumerator.Reset

                position = -1

            End Sub

        End Class
    End Class

    ''' <summary>
    ''' 表示Matrix对象中列的集合
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MatrixColumnCollection
        Implements IEnumerable

        Private _matrix As Matrix

        Friend Sub New(ByRef matrix As Matrix)

            _matrix = matrix

        End Sub

        ''' <summary>
        ''' 返回一个整数，它表示此实例对应的矩阵的列数
        ''' </summary>
        ''' <value></value>
        ''' <returns>一个整数，表示集合中元素的个数</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Count As Integer
            Get

                Return _matrix._elem.GetLength(1)

            End Get
        End Property

        ''' <summary>
        ''' 按列号返回一个列向量
        ''' </summary>
        ''' <param name="index">列号，从1开始</param>
        ''' <value></value>
        ''' <returns>一个向量，表示矩阵中指定列号的列向量</returns>
        ''' <remarks></remarks>
        Default Public ReadOnly Property Item(ByVal index As Integer) As Vector
            Get

                If (index <= 0) OrElse (index > _matrix._elem.GetLength(1)) Then

                    Throw New ArgumentException("列的序号超出矩阵范围", "index")

                End If

                Dim e(_matrix._elem.GetLength(0) - 1) As Double

                For i As Integer = 0 To _matrix._elem.GetLength(0) - 1

                    e(i) = _matrix._elem(i, index - 1)

                Next

                Return New Vector(e)

            End Get
        End Property

        ''' <summary>
        ''' 获取此实例对应的Matrix类的实例
        ''' </summary>
        ''' <value></value>
        ''' <returns>一个Matrix类的实例</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Matrix As Matrix
            Get

                Return _matrix

            End Get
        End Property

        ''' <summary>
        ''' 在指定的列之前插入指定的列向量
        ''' </summary>
        ''' <param name="column">要插入矩阵的列向量，列向量的行数必须与被修改的矩阵一致</param>
        ''' <param name="index">要将列向量插入的列号，从1开始</param>
        ''' <remarks></remarks>
        Public Sub InsertAt(ByRef column As Vector, ByVal index As Integer)

            If column Is Nothing Then

                Throw New ArgumentException("未将对象引用设置到实例", "column")

            End If

            Dim ColumnCounts As Integer = _matrix._elem.GetLength(1)

            If (index <= 0) Or (index > ColumnCounts + 1) Then

                Throw New ArgumentException("列号超出矩阵范围", "index")

            End If

            Dim RowCounts As Integer = _matrix._elem.GetLength(0)

            If column.Dimensions <> RowCounts Then

                Throw New ArgumentException("列向量的行数与矩阵不一致", "column")

            End If

            ReDim Preserve _matrix._elem(RowCounts - 1, ColumnCounts)

            For i As Integer = ColumnCounts To index Step -1

                For j As Integer = 0 To RowCounts - 1

                    _matrix._elem(j, i) = _matrix._elem(j, i - 1)

                Next

            Next

            For j As Integer = 0 To RowCounts - 1

                _matrix._elem(j, index - 1) = column(j + 1)

            Next

        End Sub

        ''' <summary>
        ''' 从矩阵中删除指定的列
        ''' </summary>
        ''' <param name="index">要删除的列号</param>
        ''' <remarks>如果矩阵只有一列，则此方法没有任何效果，也不会引发异常</remarks>
        Public Sub RemoveAt(ByVal index As Integer)

            If (index <= 0) OrElse (index > _matrix._elem.GetLength(1)) Then

                Throw New ArgumentException("列号超出矩阵范围", "index")

            End If

            For i As Integer = index - 1 To _matrix._elem.GetLength(1) - 1

                For j As Integer = 0 To _matrix._elem.GetLength(0) - 1

                    _matrix._elem(j, i) = _matrix._elem(j, i + 1)

                Next

            Next

        End Sub

        ''' <summary>
        ''' 返回一个循环访问集合的枚举器
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator

            Return New MatrixColumnEnum(_matrix)

        End Function

        Public Class MatrixColumnEnum
            Implements IEnumerator

            Private _matrix As Matrix

            Private position As Integer = -1

            Public Sub New(ByRef matrix As Matrix)

                _matrix = matrix

            End Sub

            Public ReadOnly Property Current As Object Implements System.Collections.IEnumerator.Current
                Get

                    Try

                        Return _matrix.Columns(position + 1)

                    Catch ex As Exception

                        Throw New InvalidOperationException()

                    End Try

                End Get
            End Property

            Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext

                position = position + 1

                Return (position < _matrix._elem.GetLength(1))

            End Function

            Public Sub Reset() Implements System.Collections.IEnumerator.Reset

                position = -1

            End Sub

        End Class

    End Class

    Public Shared Operator -(ByVal matrix As Matrix) As Matrix

        Return -1 * matrix

    End Operator

    Public Shared Operator +(ByVal matrix1 As Matrix, ByVal matrix2 As Matrix) As Matrix

        If matrix1 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix1")

        End If

        If matrix2 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix2")

        End If

        Return matrix1.Add(matrix2)

    End Operator

    Public Shared Operator -(ByVal matrix1 As Matrix, ByVal matrix2 As Matrix) As Matrix

        If matrix1 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix1")

        End If

        If matrix2 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix2")

        End If

        Return matrix1.Subtract(matrix2)

    End Operator

    Public Shared Operator *(ByVal scalar As Double, ByVal matrix As Matrix) As Matrix

        If matrix Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix")

        End If

        Return matrix.ScalarMultiply(scalar)

    End Operator

    Public Shared Operator *(ByVal matrix As Matrix, ByVal scalar As Double) As Matrix

        If matrix Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix")

        End If

        Return matrix.ScalarMultiply(scalar)

    End Operator

    Public Shared Operator *(ByVal matrix1 As Matrix, ByVal matrix2 As Matrix) As Matrix

        If matrix1 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix1")

        End If

        If matrix2 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix2")

        End If

        Return matrix1.Multiply(matrix2)

    End Operator

    Public Shared Operator =(ByVal matrix1 As Matrix, ByVal matrix2 As Matrix) As Boolean

        If matrix1 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix1")

        End If

        If matrix2 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix2")

        End If

        Return matrix1.Equals(matrix2)

    End Operator

    Public Shared Operator <>(ByVal matrix1 As Matrix, ByVal matrix2 As Matrix) As Boolean

        If matrix1 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix1")

        End If

        If matrix2 Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix2")

        End If

        Return Not matrix1.Equals(matrix2)

    End Operator

    Public Shared Widening Operator CType(ByVal matrix As Vector) As Matrix

        If matrix Is Nothing Then

            Return Nothing

        End If

        Dim e(0, matrix.Dimensions - 1) As Double

        For i As Integer = 0 To matrix.Dimensions - 1

            e(0, i) = matrix(i)

        Next

        Return New Matrix(e)

    End Operator

    Public Shared Narrowing Operator CType(ByVal matrix As Matrix) As Vector

        If matrix Is Nothing Then

            Throw New ArgumentException("未将对象引用设置到实例", "matrix")

        End If

        If matrix._elem.GetLength(0) = 1 Then

            Return matrix.Rows(1)

        ElseIf matrix._elem.GetLength(1) = 1 Then

            Return matrix.Columns(1)

        Else

            Throw New ArgumentException("无法将矩阵转换为向量", "matrix")

        End If

    End Operator

End Class
