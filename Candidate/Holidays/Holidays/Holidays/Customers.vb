Imports System.IO

Public Class Customers
    Private Structure Customer
        Public ClientID As String
        Public FName As String
        Public LName As String
        Public Address As String                  'Creating the structure that will hold the  data.
        Public Postcode As String
        Public PhoneNumber As String
        Public DOB As Date
    End Structure
    Private Sub Customers_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Dir$("Customers.txt") = "" Then
            Dim sw As New StreamWriter("Customers.txt", True)    'This makes sure there is actually a database to enter/read data. If not, it creates a new blank one.
            sw.WriteLine("")
            sw.Close()
            MsgBox("A new file has been created", vbExclamation, "Warning!") 'Tells the user that a blank file has been created.
        End If

        Dim CustomerData() As String = File.ReadAllLines("Customers.txt")
        For i = 0 To UBound(CustomerData) 'loops for the amount of records are stored in the file.

            If Trim(Mid(CustomerData(i), 1, 50)) = "" Then 'If there is a empty record it is ignored.

            Else
                If CInt(Trim(Mid(CustomerData(i), 1, 50))) >= CInt(txtClientID.Text) Then 'when the program is run it checks the client ids that are already on record and sets the next record to have a client id incremented by 1
                    txtClientID.Text = CInt(Trim(Mid(CustomerData(i), 1, 50))) + 1 'incrementing the client id.
                End If
            End If

        Next i

    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click

        Dim Counter As Integer
        If txtFName.Text.Replace(" ", "") = "" Then Counter = Counter + 1
        If txtLName.Text.Replace(" ", "") = "" Then Counter = Counter + 1
        If txtAddress.Text.Replace(" ", "") = "" Then Counter = Counter + 1
        If txtPostcode.Text.Replace(" ", "") = "" Then Counter = Counter + 1
        If txtPhoneNumber.Text.Replace(" ", "") = "" Then Counter = Counter + 1 ' presence check to make sure that all the required details have been entered and if they have not to not continue with the program and notify the user.
        If Counter > 0 Then 'if there is one or more missing fields
            MsgBox("You have not entered all the needed fields!") ' notifying the user that they are missing data
            Exit Sub
        End If

        If txtFName.Text.Length > 50 Then ' length check to make sure that they have entered a field that doesnt have 51 or more characters in
            MsgBox("First Name is too long! Please enter a valid First Name.")
            Exit Sub
        End If
        If txtLName.Text.Length > 50 Then ' length check to make sure that they have entered a field that doesnt have 51 or more characters in
            MsgBox("Last Name is too long! Please enter a valid Last Name.")
            Exit Sub
        End If
        If txtAddress.Text.Length > 50 Then ' length check to make sure that they have entered a field that doesnt have 51 or more characters in
            MsgBox("The Address you have entered is too long! Please enter a valid Address.")
            Exit Sub
        End If

        If (txtPostcode.Text.Replace(" ", "")).Length > 7 Then ' length check to make sure that they have entered a field that doesnt have 8 or more characters in
            MsgBox("The Postcode you have entered is too long! Please enter a valid Postcode.")
            Exit Sub
        ElseIf (txtPostcode.Text.Replace(" ", "")).Length < 7 Then ' length check to make sure that they have entered a field that doesnt have 8 or more characters in
            MsgBox("The Postcode you have entered is too short! Please enter a valid Postcode.")
            Exit Sub
        End If

        Dim PhoneNumber As String
        PhoneNumber = CStr(txtPhoneNumber.Text.Replace(" ", ""))
        If IsNumeric(PhoneNumber) = False Then 'Data type check to make sure that the phone number entered is integers
            MsgBox("The phone number you have entered has forign characters in. Please enter a valid Phone Number.")
        End If

        If (PhoneNumber).Length > 11 Then ' length check to make sure that they have entered a field that doesnt have 12 or more characters in
            MsgBox("The Phone Number you have entered is too long! Please enter a valid Phone Number.")
            Exit Sub
        ElseIf (PhoneNumber).Length < 11 Then ' length check to make sure that they have entered a field that doesnt have 10 or less characters in
            MsgBox("The Phone Number you have entered is too short! Please enter a valid Phone Number.")
            Exit Sub
        End If

        If dtpDOB.Value.Year > Date.Now.Year Then ' range check to make sure that the user hasn't entered a birth date in the future.
            MsgBox("The date of birth you have entered is in the future! Please enter a valid Date of Birth.")
        End If

        Dim countgot As Integer
        Dim CustomerDatas() As String = File.ReadAllLines("Customers.txt")
        For i = 0 To UBound(CustomerDatas)
            If Trim(Mid(CustomerDatas(i), 301, 50)) = "" Then
            Else


                countgot = 0
                If Trim(Mid(CustomerDatas(i), 51, 50)) = txtFName.Text Then countgot = countgot + 1
                If Trim(Mid(CustomerDatas(i), 101, 50)) = txtLName.Text Then countgot = countgot + 1
                If Trim(Mid(CustomerDatas(i), 151, 50)) = txtAddress.Text Then countgot = countgot + 1 'Checking if any records match the record beign entered.
                If Trim(Mid(CustomerDatas(i), 201, 50)) = txtPostcode.Text Then countgot = countgot + 1
                If Trim(Mid(CustomerDatas(i), 251, 50)) = txtPhoneNumber.Text Then countgot = countgot + 1
                If Trim(Mid(CustomerDatas(i), 301, 50)) = dtpDOB.Value Then countgot = countgot + 1
                If countgot = 6 Then
                    MsgBox("Record is already in database.") 'if there is a match the user is notified
                    Exit Sub
                End If
            End If
        Next i

        Dim CustomerData As New Customer
        Dim sw As New System.IO.StreamWriter("Customers.txt", True)
        CustomerData.ClientID = LSet(txtClientID.Text, 50)
        CustomerData.FName = LSet(txtFName.Text, 50)
        CustomerData.LName = LSet(txtLName.Text, 50)
        CustomerData.Address = LSet(txtAddress.Text, 50)                      'Filling the structure with data.
        CustomerData.Postcode = LSet(txtPostcode.Text, 50)
        CustomerData.PhoneNumber = LSet(txtPhoneNumber.Text, 50)
        CustomerData.DOB = LSet(dtpDOB.Value, 50)

        sw.WriteLine(CustomerData.ClientID & CustomerData.FName & CustomerData.LName & CustomerData.Address & CustomerData.Postcode & CustomerData.PhoneNumber & CustomerData.DOB) 'Outputing the data collected from the user into a file with padding so it can be understood later on in the program using the Trim Function.
        sw.Close()                                                                  'Always need to close afterwards
        MsgBox("File Saved!") 'notifying the user of a record being successfully added

        For i = 0 To UBound(CustomerDatas)

            If Trim(Mid(CustomerDatas(i), 1, 50)) = "" Then 'If there is a empty record it is ignored.

            Else
                If CInt(Trim(Mid(CustomerDatas(i), 1, 50))) >= CInt(txtClientID.Text) Then 'when the program is run it checks the client ids that are already on record and sets the next record to have a client id incremented by 1
                    txtClientID.Text = CInt(Trim(Mid(CustomerDatas(i), 1, 50))) + 1 'incrementing the client id by one
                End If
            End If

        Next i



    End Sub

    Private Sub btnRetreive_Click(sender As System.Object, e As System.EventArgs) Handles btnRetreive.Click

        btnSave.Visible = False
        btnBack.Visible = True
        btnRetreive.Visible = False
        txtAddress.Enabled = False
        txtFName.Enabled = False
        txtLName.Enabled = False
        txtPhoneNumber.Enabled = False
        txtPostcode.Enabled = False
        dtpDOB.Enabled = False   'Setting it so the user cannot change the data being output and displayed.

        cmbClientID.Visible = True


        Dim CustomerData() As String = File.ReadAllLines("Customers.txt")
        For i = 0 To UBound(CustomerData)
            cmbClientID.Items.Add(Trim(Mid(CustomerData(i), 1, 50))) ' adding all the currently saved client ids in the data base to a combo box where you will be able to select one and retrieve the data assosiated with the client id.
        Next i





    End Sub

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click


        btnSave.Visible = True
        btnBack.Visible = False
        btnRetreive.Visible = True
        txtAddress.Enabled = True
        txtFName.Enabled = True
        txtLName.Enabled = True
        txtPhoneNumber.Enabled = True
        txtPostcode.Enabled = True
        dtpDOB.Enabled = True  'resetting the program ready for a new record to be saved.

        txtAddress.Text = ""
        txtFName.Text = ""
        txtLName.Text = ""
        txtPhoneNumber.Text = ""
        txtPostcode.Text = "" 'resetting the textboxes for a new record to be saved.
        dtpDOB.Value = Date.Now()


        cmbClientID.Visible = False

        cmbClientID.Items.Clear() 'clearing the items from the combobox ready if the user decides to go back and retrieve client ids in the same session.


        Dim CustomerData() As String = File.ReadAllLines("Customers.txt")
        For i = 0 To UBound(CustomerData)

            If Trim(Mid(CustomerData(i), 1, 50)) = "" Then 'If there is a empty record it is ignored.

            Else
                If CInt(Trim(Mid(CustomerData(i), 1, 50))) >= CInt(txtClientID.Text) Then 'when the program is run it checks the client ids that are already on record and sets the next record to have a client id incremented by 1
                    txtClientID.Text = CInt(Trim(Mid(CustomerData(i), 1, 50))) + 1 'incrementing the client id by one
                End If
            End If

        Next i


    End Sub

    Private Sub cmbClientID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbClientID.SelectedIndexChanged
        Dim CustomerDatas() As String = File.ReadAllLines("Customers.txt")
        For i = 0 To UBound(CustomerDatas)
            If cmbClientID.Text = "" Then 'If the user hasn't chosen a client id dont run any code.

            Else
                If cmbClientID.Text = Trim(Mid(CustomerDatas(i), 1, 50)) Then ' when the user has selected a client if retrieve all the data assosiated with that specific client id and output them in textboxes.
                    txtClientID.Text = cmbClientID.Text
                    txtFName.Text = Trim(Mid(CustomerDatas(i), 51, 50))
                    txtLName.Text = Trim(Mid(CustomerDatas(i), 101, 50))
                    txtAddress.Text = Trim(Mid(CustomerDatas(i), 151, 50))
                    txtPostcode.Text = Trim(Mid(CustomerDatas(i), 201, 50))
                    txtPhoneNumber.Text = Trim(Mid(CustomerDatas(i), 251, 50))
                    dtpDOB.Value = Trim(Mid(CustomerDatas(i), 301, 50))
                End If
            End If

        Next i





    End Sub
End Class
