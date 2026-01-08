#include <stdio.h>
#include <string.h>
#include <gtk/gtk.h>

//Code chunks taken from the GOAT of this stuff Michael Labbe, Thanks Mike!
//https://github.com/mlabbe/nativefiledialog/blob/master/src/nfd_gtk.c


char* currentCharPointer;

void waitForCleanup()
{
    while (gtk_events_pending())
        gtk_main_iteration();
}

char* activate() {
    if (!gtk_init_check(NULL, NULL)) {
        return NULL;
    }

    GtkWidget* filew;
    filew = gtk_file_chooser_dialog_new ("File selection",
                                            NULL,
                                            GTK_FILE_CHOOSER_ACTION_OPEN,
                                            "_Cancel", GTK_RESPONSE_CANCEL,
                                            "_Open", GTK_RESPONSE_ACCEPT,
                                            NULL );

    char* filename;
    if ( gtk_dialog_run( GTK_DIALOG(filew) ) == GTK_RESPONSE_ACCEPT )
    {
        filename = gtk_file_chooser_get_filename( GTK_FILE_CHOOSER(filew) );
    }
    else {
        filename = NULL;
    }

    waitForCleanup();
    gtk_widget_destroy(filew);
    waitForCleanup();
    currentCharPointer = filename;
    return filename;
}

void freeCharResult() {
    if (currentCharPointer != NULL)
    {
        free(currentCharPointer);
    }
}

int makeTestWindow() {
    GtkApplication* app;
    int status;

    app = gtk_application_new("org.gtk.example", G_APPLICATION_DEFAULT_FLAGS);
    g_signal_connect(app, "activate", G_CALLBACK(activate), NULL);
    status = g_application_run(G_APPLICATION(app), 0, NULL);
    g_object_unref(app);


    return status;
}

int main(int argc, char** argv) {
    char* returnString = activate();
    if (returnString != NULL)
        printf("%s\n", returnString);
}