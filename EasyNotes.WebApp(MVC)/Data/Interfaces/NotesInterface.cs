using EasyNotes.WebApp_MVC_.Data.Entities;

namespace EasyNotes.WebApp_MVC_.Data.Interfaces
{
    public interface INotesInterface
    {
        /* CreateNote(string name, string content);
         AssignACategoryToNote(string categoty, string note);
         Result UpdateNote(string oldnNote, string newNote);
         Result DeleteNote(string name, string userNameId);*/
        List<Note> FilterByCategory(string categoryName);
        List<Note> FilterByNoteName(string userNameId);
        List<Note> FilterNoteByCategory(string nameId);
    }
}
