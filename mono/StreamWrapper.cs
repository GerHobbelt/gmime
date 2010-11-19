using System;
using System.IO;

namespace GMime {
	public class StreamWrapper : System.IO.Stream, IDisposable {
		GMime.Stream stream;
		
		public StreamWrapper (GMime.Stream stream)
		{
			if (stream == null)
				throw new ArgumentNullException ();
			
			this.stream = stream;
		}
		
		~StreamWrapper ()
		{
			Close ();
		}
		
		public new void Dispose ()
		{
			Close ();
		}
		
		public GMime.Stream GMimeStream {
			get { return stream; }
			set { 
				if (value == null)
					throw new ArgumentNullException ();
				
				stream = value;
			}
		}
		
		public override bool CanRead {
			get { return stream == null ? false : true; }
		}
		
		public override bool CanSeek {
			get { return stream == null || stream is StreamFilter ? false : true; }
		}
		
		// FIXME: Support writing?
		public override bool CanWrite {
			get { return false; }
		}
		
		public override long Length {
			get { 
				if (stream == null)
					throw new ObjectDisposedException ("GMimeStream", "The stream has been closed");
				
				return stream.Length;
			}
		}
		
		public override long Position {
			get { 
				if (stream == null)
					throw new ObjectDisposedException ("GMimeStream", "The stream has been closed");
				
				return stream.Tell ();
			}
			
			set {
				if (Seek (value, SeekOrigin.Begin) == -1)
					throw new IOException ();
			}
		}
		
		public override void Close ()
		{
			if (stream == null)
				throw new ObjectDisposedException ("GMimeStream", "The stream has been closed");
			
			stream.Dispose ();
			stream = null;
		}
		
		public override void Flush ()
		{
			stream.Flush ();
		}
		
		public override int Read (byte[] buffer, int offset, int count)
		{
			if (stream == null)
				throw new ObjectDisposedException ("GMimeStream", "The stream has been closed");
			
			if (offset != 0)
				throw new ArgumentOutOfRangeException ("offset must be 0");
			
			int nread = (int) stream.Read (buffer, (uint) count);
			
			if (nread < 0)
				return 0;
			
			return nread;
		}

		public override long Seek (long offset, SeekOrigin origin)
		{
			if (stream == null)
				throw new ObjectDisposedException ("GMimeStream", "The stream has been closed");
			
			if (stream is StreamFilter) {
				if (offset != 0 || origin != SeekOrigin.Begin)
					throw new NotSupportedException ();
				
				// offset = 0 is a special case and means reset the streamfilter
				((StreamFilter) stream).Reset ();
				return 0;
			}
			
			long ret = -1;
			
			switch (origin) {
			case SeekOrigin.Begin:
				if (offset < 0)
					throw new ArgumentOutOfRangeException ();
				
				ret = stream.Seek (offset, GMime.SeekWhence.Set);
				break;
			case SeekOrigin.Current:
				ret = stream.Seek (offset, GMime.SeekWhence.Cur);
				break;
			case SeekOrigin.End:
				if (offset > 0)
					throw new ArgumentOutOfRangeException ();
				
				ret = stream.Seek (offset, GMime.SeekWhence.End);
				break;
			}
			
			if (ret == -1)
				throw new IOException ();
			
			return ret;
		}
		
 		public override void SetLength (long value)
		{
			throw new NotSupportedException ();
		}
		
		public override void Write (byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException ();
		}
	}
}
