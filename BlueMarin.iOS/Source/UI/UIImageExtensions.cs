using System;
using UIKit;
using CoreImage;
using System.Drawing;
using CoreGraphics;

namespace BlueMarin.iOS
{
	public static class UIImageExtensions
	{
		// https://gist.github.com/foxxjnm/e452f2aebc2f6a01874b
		public static UIImage Blur(this UIImage image, float blurRadius = 25f)
		{
			if (image != null)
			{
				// Create a new blurred image.
				var imageToBlur = new CIImage (image);
				var blur = new CIGaussianBlur ();
				blur.Image = imageToBlur;
				blur.Radius = blurRadius;

				var blurImage = blur.OutputImage;
				var context = CIContext.FromOptions (new CIContextOptions { UseSoftwareRenderer = false });
				var cgImage = context.CreateCGImage (blurImage, new RectangleF (0f, 0f, (float)image.Size.Width, (float)image.Size.Height));
				var newImage = UIImage.FromImage (cgImage);

				// Clean up
				imageToBlur.Dispose ();
				context.Dispose ();
				blur.Dispose ();
				blurImage.Dispose ();
				cgImage.Dispose ();

				return newImage;
			}
			return null;
		}

		// https://gist.github.com/nicwise/890460
		public static UIImage ScaleImage(this UIImage image, int maxSize)
		{
			UIImage res;

			using (CGImage imageRef = image.CGImage)
			{
				CGImageAlphaInfo alphaInfo = imageRef.AlphaInfo;
				CGColorSpace colorSpaceInfo = CGColorSpace.CreateDeviceRGB();
				if (alphaInfo == CGImageAlphaInfo.None)
				{
					alphaInfo = CGImageAlphaInfo.NoneSkipLast;
				}

				nint width, height;

				width = imageRef.Width;
				height = imageRef.Height;


				if (height >= width)
				{
					width = (int)Math.Floor((double)width * ((double)maxSize / (double)height));
					height = maxSize;
				}
				else
				{
					height = (int)Math.Floor((double)height * ((double)maxSize / (double)width));
					width = maxSize;
				}


				CGBitmapContext bitmap;

				if (image.Orientation == UIImageOrientation.Up || image.Orientation == UIImageOrientation.Down)
				{
					bitmap = new CGBitmapContext(IntPtr.Zero, width, height, imageRef.BitsPerComponent, imageRef.BytesPerRow, colorSpaceInfo, alphaInfo);
				}
				else
				{
					bitmap = new CGBitmapContext(IntPtr.Zero, height, width, imageRef.BitsPerComponent, imageRef.BytesPerRow, colorSpaceInfo, alphaInfo);
				}

				switch (image.Orientation)
				{
				case UIImageOrientation.Left:
					bitmap.RotateCTM((float)Math.PI / 2);
					bitmap.TranslateCTM(0, -height);
					break;
				case UIImageOrientation.Right:
					bitmap.RotateCTM(-((float)Math.PI / 2));
					bitmap.TranslateCTM(-width, 0);
					break;
				case UIImageOrientation.Up:
					break;
				case UIImageOrientation.Down:
					bitmap.TranslateCTM(width, height);
					bitmap.RotateCTM(-(float)Math.PI);
					break;
				}

				bitmap.DrawImage(new CGRect(0, 0, width, height), imageRef);


				res = UIImage.FromImage(bitmap.ToImage());
				bitmap = null;

			}

			return res;
		}
	}
}

