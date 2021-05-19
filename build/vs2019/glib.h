/**
 * glib.h: Emulation of some GNOME APIs
 **/
#pragma once

#include <stdint.h>
#include <assert.h>

#define G_BEGIN_DECLS    
#define G_END_DECLS      

#define G_GNUC_PRINTF(m, n)		// printf style args spec attribute
#define G_GNUC_INTERNAL
#define G_GNUC_CONST

#ifndef TRUE
#define TRUE (1)
#endif
#ifndef FALSEE
#define FALSE (0)
#endif
#ifndef MAX
#define MAX(a, b) ((a) > (b) ? (a) : (b))
#endif

#define GLIB_CHECK_VERSION(u, v, w)		TRUE

typedef int gboolean;
typedef unsigned int guint;
typedef int64_t gint64;
typedef uint32_t guint32;
typedef char gchar;
typedef void* gpointer;
typedef const void* gconstpointer;

typedef enum {
	G_FOO,
	G_BAR,
} GType;

typedef const char * GQuark;

typedef struct {
	int hhh;
} GHashTable;

typedef struct {
	int arr;
} GPtrArray;

typedef struct {
	int sss;
} GString;

typedef struct {
	int ddt;
} GDateTime;

typedef struct {
	int bbb;
} GBytes;

typedef struct {
	int errrr;
} GError;

typedef struct {
	int argh;
} GByteArray;

typedef struct {
	int ioio;
} GOutputStream;

typedef struct {
	int jojo;
} GInputStream;

typedef struct {
	int fff;
} GFile;

typedef struct {
	int chkkk;
} GChecksum;

typedef enum {
	FUBAR,
	FUNK,
} GChecksumType;

typedef void GClassInitFunc_f(void* class_ref);
typedef void GInstanceInitFunc_f(void* object_ref);

typedef GClassInitFunc_f *GClassInitFunc;
typedef GInstanceInitFunc_f *GInstanceInitFunc;

typedef struct {
	size_t class_size;
	void* base_class_init;
	void* base_class_finalize;
	GClassInitFunc class_init;
	void* class_finalize;
	void* class_data;
	size_t object_sizee;
	int n_preallocs;
	GInstanceInitFunc object_init;
} GTypeInfo;

#define G_OBJECT_CLASS(class_def)     class_def

#define G_DEFINE_BOXED_TYPE(T, constructor, cloner, destructor)

#define G_TYPE_CHECK_INSTANCE_TYPE(obj, type_id)	TRUE
#define G_TYPE_CHECK_CLASS_CAST(class_ref, type_id, type_def)			(class_ref)

typedef int iconv_t;

#define g_quark_from_static_string(str)			str

#define g_ascii_strcasecmp(a, b)		_stricmp(a, b)

#define g_return_val_if_fail(cond, retval)		if (!(cond)) return (retval)
#define g_assert_not_reached()					assert(!"Should never be reached!")

void g_set_error(GError** err, GQuark quark, int errcode, const char* msg);
void g_set_error_literal(GError** err, GQuark quark, int errcode, const char* msg);
