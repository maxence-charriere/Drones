using Drones.Infrastructure;
using FFmpeg.AutoGen;
using System;

namespace Drones.ARDrone.Client.Video
{
    public unsafe class VideoDecoder : DisposableBase
    {
        // @Public
        public VideoDecoder()
        {
            AVCodec* pCodec = FFmpegInvoke.avcodec_find_decoder(CodecId);
            if (pCodec == null)
            {
                throw new VideoDecoderException("Unsupported codec.");
            }

            _pDecodingContext = FFmpegInvoke.avcodec_alloc_context3(pCodec);

            if (FFmpegInvoke.avcodec_open2(_pDecodingContext, pCodec, null) < 0)
            {
                throw new VideoDecoderException("Could not open codec.");
            }
        }

        public bool TryDecode(ref AVPacket packet, ref AVFrame frame)
        {
            int gotPicture;
            fixed (AVPacket* pPacket = &packet)
            fixed (AVFrame* pFrame = &frame)
            {
                int decodedSize = FFmpegInvoke.avcodec_decode_video2(_pDecodingContext, pFrame, &gotPicture, pPacket);
                if (decodedSize < 0)
                {
                    System.Diagnostics.Debug.WriteLine("Error while decoding frame.");
                }
            }
            return gotPicture == 1;
        }


        // @Protected
        protected override void DisposeOverride()
        {
            FFmpegInvoke.avcodec_close(_pDecodingContext);
        }


        // @Private
        const AVCodecID CodecId = AVCodecID.AV_CODEC_ID_H264;
        readonly AVCodecContext* _pDecodingContext;

        static VideoDecoder()
        {
            FFmpegInvoke.av_register_all();
            FFmpegInvoke.avcodec_register_all();
        }
    }
}
