using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnKey : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip melody;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = 0.6f;

        // C、E、G、A、G、E、D、C。
        float[] notes =
        {
            523.25f, 659.25f, 783.99f, 880f,
            783.99f, 659.25f, 587.33f, 523.25f
        };

        int sampleRate = 44100;
        float noteInterval = 0.32f;
        float noteDuration = 0.65f;

        int totalSamples = Mathf.CeilToInt(
            ((notes.Length - 1) * noteInterval + noteDuration)
            * sampleRate
        );

        float[] samples = new float[totalSamples];

        for (int n = 0; n < notes.Length; n++)
        {
            int start = Mathf.RoundToInt(n * noteInterval * sampleRate);
            int length = Mathf.RoundToInt(noteDuration * sampleRate);

            for (int i = 0; i < length && start + i < totalSamples; i++)
            {
                float t = (float)i / sampleRate;
                float phase = 2f * Mathf.PI * notes[n] * t;

                // 主音加少量泛音，产生柔和的钟琴音色。
                float tone = Mathf.Sin(phase)
                           + 0.25f * Mathf.Sin(phase * 2f)
                           + 0.08f * Mathf.Sin(phase * 3f);

                // 柔和起音，自然衰减，尾部平滑结束。
                float attack = Mathf.Clamp01(t / 0.015f);
                float decay = Mathf.Exp(-5f * t);
                float release = Mathf.Clamp01(
                    (noteDuration - t) / 0.08f
                );

                samples[start + i] +=
                    tone * attack * decay * release * 0.3f;
            }
        }

        melody = AudioClip.Create(
            "GentleMelody", totalSamples, 1, sampleRate, false
        );
        melody.SetData(samples, 0);
        audioSource.clip = melody;
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.mKey.wasPressedThisFrame)
        {
            // 再次按 M 会从头播放，避免多段声音叠加。
            audioSource.Stop();
            audioSource.Play();
        }
    }

    void OnDestroy()
    {
        if (melody != null)
            Destroy(melody);
    }
}
