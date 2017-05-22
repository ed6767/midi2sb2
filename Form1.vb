Imports System.IO
Imports Newtonsoft.Json
Imports NAudio.Midi

Public Class Form1

    ''' <summary>
    ''' The name used for saving the converted data.
    ''' </summary>
    Private saveName As String = "Data"

    ''' <summary>
    ''' The open midi file.
    ''' </summary>
    Private midi As MidiFile

    ''' <summary>
    ''' The list of channels within the midi file. (0 - 16, excluding drum tracks)
    ''' </summary>
    Private Channels As New List(Of Integer)()

    ''' <summary>
    ''' The list of notes within the midi file.
    ''' </summary>
    Private Notes As New List(Of NoteOnEvent)()

    ''' <summary>
    ''' The list of tempo changes within the midi file.
    ''' </summary>
    Private TempoChanges As New List(Of TempoEvent)()

    ''' <summary>
    ''' The list of instruments supported by Scratch.
    ''' </summary>
    Private ScratchVoices As String() = New String(20) {"Piano", "Electric Piano", "Organ", "Guitar", "Electric Guitar", "Bass",
            "Pizzicato", "Cello", "Trombone", "Clarinet", "Saxaphone", "Flute",
            "Wooden Flute", "Bassoon", "Choir", "Vibraphone", "Music Box", "Steel Drum",
            "Marimba", "Synth Lead", "Synth Pad"}

    ''' <summary>
    ''' Initialises the form.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Occurs when the load button is clicked.
    ''' </summary>
    Private Sub BtnLoad_Click()
        ' Create and display dialog
        Dim dialog As New OpenFileDialog()
        dialog.Filter = "MIDI Files (*.mid) | *.mid"
        Dim result As DialogResult = dialog.ShowDialog()
        If result = DialogResult.OK Then
            ' File selected so load midi file
            LoadMidi(dialog.FileName)
        End If
    End Sub

    ''' <summary>
    ''' Loads the notes and tempo changes from the specified midi file.
    ''' </summary>
    ''' <param name="file">The file path of the midi file to upload.</param>
    Private Sub LoadMidi(file As String)
        Try
            ' Display file name
            FileLbl.Text = Path.GetFileName(file)

            ' Open midi file
            midi = New MidiFile(file, False)

            ' Create save name for midi file
            saveName = Path.GetFileNameWithoutExtension(file)

            ' Resets the list of channels
            Channels = New List(Of Integer)()
            ChannelTable.Rows.Clear()

            ' The list of voices used in the midi file
            Dim PatchChanges As New List(Of PatchChangeEvent)()

            ' For every track/channel
            For Each track As IList(Of MidiEvent) In midi.Events
                ' For every midi event (note, metadata, etc.)
                For Each midiEvent As MidiEvent In track
                    Select Case midiEvent.CommandCode
                        Case MidiCommandCode.NoteOn
                            ' Note
                            Dim ne As NoteOnEvent = DirectCast(midiEvent, NoteOnEvent)
                            If ne.OffEvent IsNot Nothing Then
                                Notes.Add(ne)
                            End If

                            If Not Channels.Contains(ne.Channel) AndAlso ne.Channel <> 16 AndAlso ne.Channel <> 10 Then
                                ' New instument channel discovered so add to channel list
                                Channels.Add(ne.Channel)
                            End If

                            Exit Select
                        Case MidiCommandCode.MetaEvent
                            ' Metadata
                            Dim [me] As MetaEvent = DirectCast(midiEvent, MetaEvent)
                            Select Case [me].MetaEventType
                                Case MetaEventType.SetTempo
                                    ' Tempo Change
                                    TempoChanges.Add(DirectCast([me], TempoEvent))
                                    Exit Select
                            End Select
                            Exit Select
                        Case MidiCommandCode.PatchChange
                            ' Patch (instrument)
                            PatchChanges.Add(DirectCast(midiEvent, PatchChangeEvent))
                            Exit Select
                    End Select
                Next
            Next

            ' Sort channels numerically
            Channels.Sort()

            ' Add rows to ChannelTable (TODO: refactor code)

            ' For each channel
            For Each channel As Integer In Channels
                ' Identify MIDI voice (default is acoustic grand)
                Dim instrument As String = "Piano"

                ' For each midi voice
                For Each pe As PatchChangeEvent In PatchChanges
                    If pe.Channel = channel Then
                        ' Had to change this
                        instrument = pe.ToString().Split(":")(1).Replace(pe.ToString.Split(":")(0) & ":" & pe.ToString.Split(":")(1).Split(" ")(0), "")
                    End If
                Next

                ' Add row to ChannelTable
                ChannelTable.Rows.Add(channel, instrument, False, findbestinst(instrument)) ' Change to best suited ed
            Next

            ' Finally, sort notes by position in the piece
            Notes.Sort(Function(x, y) x.AbsoluteTime.CompareTo(y.AbsoluteTime))
            TabControl1.SelectedIndex = 1
        Catch ex As Exception
            FileLbl.Text = "Error: " & ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Finds the best instrument to use.
    ''' </summary>
    Function findbestinst(ByVal midiinst As String) As String
        Dim inst = ""
        Dim oin = midiinst.ToLower
        'Each section checks each group of instruments

#Region "Pianos"

        If oin.Contains("piano") OrElse oin.Contains("grand") OrElse oin.Contains("bright acoustic") Then
            If oin.Contains("electric") Then
                inst = "Electric Piano"
                GoTo BailOut
            Else
                inst = "Piano"
                GoTo BailOut
            End If
        End If

#End Region

#Region "Guitars"

        If oin.Contains("guitar") OrElse oin.Contains("banjo") Then
            If oin.Contains("electric") Then
                inst = "Electric Guitar"
                GoTo BailOut
            Else
                inst = "Guitar"
                GoTo BailOut
            End If
        End If

#End Region

#Region "Organs, harmonicas ext.."

        If oin.Contains("organ") OrElse oin.Contains("accordian") OrElse oin.Contains("harmonica") Then

            inst = "Organ"
            GoTo BailOut

        End If

#End Region

#Region "Bass"

        If oin.Contains("bass") Then

            inst = "Bass"
            GoTo BailOut

        End If

#End Region

#Region "Stuff like Harps"

        If oin.Contains("harp") OrElse oin.Contains("pizzicato") OrElse oin.Contains("harpsichord") Then

            inst = "Pizzicato"
            GoTo BailOut

        End If

#End Region

#Region "Strings"

        If oin.Contains("violin") OrElse oin.Contains("string") OrElse oin.Contains("viola") OrElse oin.Contains("cello") Then

            inst = "Cello"
            GoTo BailOut

        End If

#End Region

#Region "Saxaphones"

        If oin.Contains("sax") OrElse oin.Contains("saxaphone") Then

            inst = "Saxaphone"
            GoTo BailOut

        End If

#End Region

#Region "Synths, voices ext.."

        If oin.Contains("lead") Then

            inst = "Synth Lead"
            GoTo BailOut

        End If
        If oin.Contains("pad") OrElse oin.Contains("voice") OrElse oin.Contains("choir") Then

            inst = "Synth Pad"
            GoTo BailOut

        End If

#End Region

#Region "Everything else"

        For Each itm As String In ListBox1.Items
            If oin.Contains(itm.ToLower) Then
                inst = itm
                GoTo BailOut
            End If
        Next

#End Region

#Region "Bail out And Return"

BailOut:
        Return inst

#End Region

    End Function

    ''' <summary>
    ''' Occurs when the save button is clicked.
    ''' </summary>
    Private Sub BtnSave_Click()
        processAndSave()
    End Sub

    ''' <summary>
    ''' Processes and saves the midi data.
    ''' </summary>
    Private Sub processAndSave()

#Region "Convert"

        ' Current time in beats of the current note
        Dim currentTime As Double = -1

        ' Notes data
        Dim NotesStart As New List(Of String)()
        ' Start times for each note (per bar)
        Dim NotesPitch As New List(Of String)()
        ' Pitch of each note (60 = middle C)
        Dim NotesDuration As New List(Of String)()
        ' Duration of each note
        Dim NotesVoice As New List(Of String)()
        ' Instrument of each notes
        Dim NotesVolume As New List(Of String)()
        ' Volume of each note
        Dim NotesIndexes As New List(Of String)()
        ' Indexes for the start of each bar in the previous data
        ' Drums data
        Dim DrumsStart As New List(Of String)()
        ' Start times for each drum (per bar)
        Dim DrumsDuration As New List(Of String)()
        ' Duration of each drum
        Dim DrumsDrum As New List(Of String)()
        ' Percussion instrument to use
        Dim DrumsVolume As New List(Of String)()
        ' Volume of each drum
        Dim DrumsIndexes As New List(Of String)()
        ' Indexes for the start of each bar in the previous data
        ' Tempo of each bar
        Dim BarTempo As New List(Of String)()

        ' Convert notes data

        ' For each note event
        For Each ne As NoteOnEvent In Notes
            ' Detect if new bar has started
            Dim currentTimeOld As Double = currentTime
            currentTime = ne.AbsoluteTime / CDbl(midi.DeltaTicksPerQuarterNote)
            If Math.Floor(currentTime / 4) > Math.Floor(currentTimeOld / 4) Then
                ' New bar, so add spacers and index
                NotesStart.Add("-")
                NotesPitch.Add("-")
                NotesDuration.Add("-")
                NotesVoice.Add("-")
                NotesVolume.Add("-")
                NotesIndexes.Add((NotesStart.Count + 1).ToString())

                DrumsStart.Add("-")
                DrumsDuration.Add("-")
                DrumsDrum.Add("-")
                DrumsVolume.Add("-")
                DrumsIndexes.Add((DrumsStart.Count + 1).ToString())

                ' Find tempo of bar (could be more efficient)
                Dim tempoEvent As Integer = 0
                While tempoEvent < TempoChanges.Count - 1 AndAlso (TempoChanges(tempoEvent).AbsoluteTime / CDbl(midi.DeltaTicksPerQuarterNote)) < currentTime
                    tempoEvent += 1
                End While

                BarTempo.Add(TempoChanges(tempoEvent).Tempo.ToString())
            End If

            If ne.Channel <> 16 AndAlso ne.Channel <> 10 Then
                ' Instrument track

                ' Find ChannelTable row relating to this channel
                Dim rowIndex As Integer = Array.IndexOf(Channels.ToArray(), ne.Channel)
                If rowIndex > -1 Then
                    Dim row As DataGridViewRow = ChannelTable.Rows(rowIndex)

                    ' Add data
                    NotesStart.Add(Math.Round(currentTime Mod 4, 2).ToString())
                    ' Modulus because start values are per bar
                    NotesPitch.Add(ne.NoteNumber.ToString())
                    NotesDuration.Add(Math.Round(ne.NoteLength / CDbl(midi.DeltaTicksPerQuarterNote), 2).ToString())

                    If CBool(row.Cells(2).Value) = False Then
                        NotesVoice.Add((Array.IndexOf(ScratchVoices, row.Cells(3).Value) + 1).ToString())
                        If row.Cells(3).Value = "" Then
                            MsgBox("One or more of your voices hasn't had an instrument assigned. Go back and find one and try again.", MsgBoxStyle.Exclamation)
                            Exit Sub
                        End If
                    Else
                        ' Track has been muted, but we're too lazy to delete the note so we just give a voice of -1
                        NotesVoice.Add("-1")
                    End If
                    NotesVolume.Add(Math.Round(ne.Velocity / CDbl(127) * 100, 2).ToString())
                End If
            Else
                ' Drum track

                ' Add data
                DrumsStart.Add(Math.Round(currentTime Mod 4, 2).ToString())
                DrumsDrum.Add(ScratchDrum(ne.NoteNumber).ToString())
                DrumsDuration.Add(Math.Round(ne.NoteLength / CDbl(midi.DeltaTicksPerQuarterNote), 2).ToString())
                DrumsVolume.Add(Math.Round(ne.Velocity / CDbl(127) * 100, 2).ToString())
            End If
        Next

        ' Add closing data to ensure correct playback (same as adding a new bar)
        NotesStart.Add("-")
        NotesPitch.Add("-")
        NotesDuration.Add("-")
        NotesVoice.Add("-")
        NotesVolume.Add("-")
        NotesIndexes.Add((NotesStart.Count + 1).ToString())

        DrumsStart.Add("-")
        DrumsDuration.Add("-")
        DrumsDrum.Add("-")
        DrumsVolume.Add("-")
        DrumsIndexes.Add((DrumsStart.Count + 1).ToString())

#End Region

#Region "Save File"

        ' Save Files

        ' Create directory
        Dim dir As String = (Convert.ToString(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MIDI Importer\") & saveName) + "\data\"
        Dim savedir As String = (Convert.ToString(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\MIDI Importer\") & saveName) + "\"
        ' Save notes
        Dim projectjson = My.Computer.FileSystem.ReadAllText("base\project.json") ' I could parse the JSON but heck, who cares?
        ' Try not to get confused here! Amazing saving bit.

        Dim fbartempo = listtojsonarraythehardway(BarTempo)

        projectjson = projectjson.Replace("""PUTBARTEMPO""", fbartempo)

        Dim fnotesstart = listtojsonarraythehardway(NotesStart)

        projectjson = projectjson.Replace("""PUTNOTESSTART""", fnotesstart)
        Dim fnotespitch = listtojsonarraythehardway(NotesPitch)

        projectjson = projectjson.Replace("""PUTNOTESPITCH""", fnotespitch)
        Dim fnotesduration = listtojsonarraythehardway(NotesDuration)

        projectjson = projectjson.Replace("""PUTNOTESDURATION""", fnotesduration)
        Dim fnotesvoice = listtojsonarraythehardway(NotesVoice)

        projectjson = projectjson.Replace("""PUTNOTESVOICE""", fnotesvoice)
        Dim fnotesvolume = listtojsonarraythehardway(NotesVolume)

        projectjson = projectjson.Replace("""PUTNOTESVOLUME""", fnotesvolume)
        Dim fnotesindex = listtojsonarraythehardway(NotesIndexes)

        projectjson = projectjson.Replace("""PUTNOTESINDEX""", fnotesindex)

        'Drums! I like drums. Drums are fun. And beatful ha ha geddit?
        Dim fdrumsstart = listtojsonarraythehardway(DrumsStart)

        projectjson = projectjson.Replace("""PUTDRUMSSTART""", fdrumsstart)

        Dim fdrumsduration = listtojsonarraythehardway(DrumsDuration)

        projectjson = projectjson.Replace("""PUTDRUMSDURATION""", fdrumsduration)

        Dim fdrumsdrum = listtojsonarraythehardway(DrumsDrum)

        projectjson = projectjson.Replace("""PUTDRUMSDRUM""", fdrumsdrum)
        Dim fdrumsvolume = listtojsonarraythehardway(DrumsVolume)

        projectjson = projectjson.Replace("""PUTDRUMSVOLUME""", fdrumsvolume)
        Dim fdrumsindex = listtojsonarraythehardway(DrumsIndexes)

        projectjson = projectjson.Replace("""PUTDRUMSINDEX""", fdrumsindex)
        My.Computer.FileSystem.CreateDirectory(savedir)
        My.Computer.FileSystem.CopyDirectory("base", dir)
        Try
            Kill("project.json")
        Catch ex As Exception

        End Try
        projectjson = projectjson.Replace("PUTSONGNAME", saveName)
        My.Computer.FileSystem.WriteAllText(dir & "project.json", projectjson, False)

        IO.Compression.ZipFile.CreateFromDirectory(dir, savedir & "converted.sb2")
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName = "" Then
            My.Computer.FileSystem.DeleteDirectory(savedir, FileIO.DeleteDirectoryOption.DeleteAllContents)
            Exit Sub
        Else
            IO.File.Copy(savedir & "converted.sb2", SaveFileDialog1.FileName)
            My.Computer.FileSystem.DeleteDirectory(savedir, FileIO.DeleteDirectoryOption.DeleteAllContents)
            Dim fi As New FileInfo(SaveFileDialog1.FileName)

            Process.Start(fi.Directory.ToString)
            Application.Exit()
        End If

#End Region

    End Sub

#Region "Various other Functions"

    Function listtojsonarraythehardway(ByVal listt As List(Of String)) As String
        Dim thearray = ""
        For Each itm As String In listt
            If listt.IndexOf(itm) = listt.Count Then
                thearray &= """" & itm & """"
            Else
                thearray &= """" & itm & """, " & vbCrLf
            End If

        Next
        Return thearray
    End Function

    ''' <summary>
    ''' Returns the Scratch drum equivalent for the specified midi drum. -1 indicates no equivalent.
    ''' </summary>
    ''' <param name="midiDrum">The midi drum note to find the equivalent of.</param>
    ''' <returns>The Scratch drum equivalent for the specified midi drum. -1 indicates no equivalent.</returns>
    Private Function ScratchDrum(midiDrum As Integer) As Integer
        Select Case midiDrum
            Case 35
                Return 2
                ' Acoustic Bass Drum -> Bass Drum
            Case 36
                Return 2
                ' Bass Drum 1        -> Bass Drum
            Case 37
                Return 3
                ' Side Stick         -> Side Stick
            Case 38
                Return 1
                ' Acoustic Snare     -> Snare Drum
            Case 39
                Return 8
                ' Hand Clap          -> Hand Clap
            Case 40
                Return 1
                ' Electric Snare     -> Snare Drum
            Case 41
                Return -1
                ' Low Floor Tom      -> Unknown
            Case 42
                Return 6
                ' Closed Hi-Hat      -> Closed Hi-Hat
            Case 43
                Return -1
                ' High Floor Tom     -> Unknown
            Case 44
                Return 5
                ' Pedal Hi-Hat       -> Open Hi-Hat
            Case 45
                Return -1
                ' Low Tom            -> Unknown
            Case 46
                Return 5
                ' Open Hi-Hat        -> Open Hi-Hat
            Case 47
                Return -1
                ' Low-Mid Tom        -> Unknown
            Case 48
                Return -1
                ' Hi-Mid Tom         -> Unknown
            Case 49
                Return 4
                ' Crash Cymbal 1     -> Crash Cymbal
            Case 50
                Return -1
                ' High Tom           -> Unknown
            Case 51
                Return 4
                ' Ride Cymbal 1      -> Crash Cymbal
            Case 52
                Return 4
                ' Chinese Cymbal     -> Crash Cymbal
            Case 53
                Return -1
                ' Ride Bell          -> Unknown
            Case 54
                Return 7
                ' Tambourine         -> Tambourine
            Case 55
                Return 4
                ' Splash Cymbal      -> Crash Cymbal
            Case 56
                Return 11
                ' Cowbell            -> Cowbell
            Case 57
                Return 4
                ' Crash Cymbal 2     -> Crash Cymbal
            Case 58
                Return 17
                ' Vibraslap          -> Vibraslap
            Case 59
                Return 4
                ' Ride Cymbal 2      -> Crash Cymbal
            Case 60
                Return 13
                ' Hi Bongo           -> Bongo
            Case 61
                Return 13
                ' Low Bongo          -> Bongo
            Case 62
                Return -1
                ' Mute Hi Conga      -> Unknown
            Case 63
                Return -1
                ' Open Hi Conga      -> Unknown
            Case 64
                Return -1
                ' Low Conga          -> Unknown
            Case 65
                Return -1
                ' High Timbale       -> Unknown
            Case 66
                Return -1
                ' Low Timbale        -> Unknown
            Case 67
                Return -1
                ' High Agogo         -> Unknown
            Case 68
                Return -1
                ' Low Agogo          -> Unknown
            Case 69
                Return 15
                ' Cabasa             -> Cabasa
            Case 70
                Return -1
                ' Maracas            -> Unknown
            Case 71
                Return -1
                ' Short Whistle      -> Unknown
            Case 72
                Return -1
                ' Long Whistle       -> Unknown
            Case 73
                Return 16
                ' Short Guiro        -> Guiro
            Case 74
                Return 16
                ' Long Guiro         -> Guiro
            Case 75
                Return 9
                ' Claves             -> Claves
            Case 76
                Return 10
                ' Hi Wood Block      -> Wood Block
            Case 77
                Return 10
                ' Low Wood Block     -> Wood Block
            Case 78
                Return 18
                ' Mute Cuica         -> Open Cuica
            Case 79
                Return 18
                ' Open Cuica         -> Open Cuica
            Case 80
                Return 12
                ' Mute Triangle      -> Triangle
            Case 81
                Return 12
            Case Else
                ' Open Triangle      -> Triangle
                Return -1
                ' Unknown            -> Unknown
        End Select
    End Function

    Private Sub SaveFile(path As String, data As List(Of String))
        Dim Writer As StreamWriter = File.CreateText(path)
        Writer.Write(String.Join(vbCr & vbLf, data.ToArray()))
        Writer.Close()
        Writer.Dispose()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles ChannelTable.CellContentClick

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        BtnLoad_Click()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        BtnSave_Click()
    End Sub

    Private Sub TabPage3_Click(sender As Object, e As EventArgs) Handles TabPage3.Click

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TabControl1_TabIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.TabIndexChanged

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 2 Then
            If Not FileLbl.Text.ToLower.Contains(".mid") Then

                TabControl1.SelectedIndex = 0
                MsgBox("Please open a file first.", MsgBoxStyle.Information)
                Exit Sub
            End If
            Height = 98
            TabControl1.Height = 174
        ElseIf TabControl1.SelectedIndex = 1 Then
            If Not FileLbl.Text.ToLower.Contains(".mid") Then

                TabControl1.SelectedIndex = 0
                MsgBox("Please open a file first.", MsgBoxStyle.Information)
                Exit Sub
            End If
            Height = 362
            TabControl1.Height = 310
        Else
            If FileLbl.Text.ToLower.Contains(".mid") Then

                TabControl1.SelectedIndex = 1
                MsgBox("File already open", MsgBoxStyle.Information)
                Exit Sub

            End If
            Height = 165
            TabControl1.Height = 112
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Form2.ShowDialog()
    End Sub

#End Region

End Class