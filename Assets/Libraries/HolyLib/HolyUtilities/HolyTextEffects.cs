using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace Holylib.Utilities
{
    public class HolyTextEffects : Singleton<HolyTextEffects>
    {
        public void ApplyShake(TMP_Text tmp_text)
        {
            if (shakeRoutines.ContainsKey(tmp_text)) StopCoroutine(shakeRoutines[tmp_text]);

            string text = tmp_text.text;

            // Set shake effect
            string pattern = @"<shake=(\d*\.?\d+)>(.*?)</shake>";
            MatchCollection matches = Regex.Matches(text, pattern);
            string[] substrings = new string[matches.Count];
            float[] intensities = new float[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                intensities[i] = float.Parse(matches[i].Groups[1].Value);
                substrings[i] = matches[i].Groups[2].Value;

                text = Regex.Replace(text, pattern, m => m.Groups[2].Value);
            }

            tmp_text.text = text;

            shakeRoutines[tmp_text] =StartCoroutine(ShakeText(tmp_text, substrings, intensities));
        }

        public void RemoveShake(TMP_Text tmp_text)
        {
            if (shakeRoutines.ContainsKey(tmp_text))
            {
                StopCoroutine(shakeRoutines[tmp_text]);
                shakeRoutines.Remove(tmp_text);
            }
        }

        private Dictionary<TMP_Text, Coroutine> shakeRoutines = new();

        private IEnumerator ShakeText(TMP_Text shakeText, string[] substrings, float[] intensities)
        {
            TMP_TextInfo textInfo = shakeText.textInfo;

            while (true)
            {
                // Force update the mesh to get the latest vertex data
                shakeText.ForceMeshUpdate();

                // Get the full text
                string fullText = shakeText.text;

                for (int j = 0; j < substrings.Length; j++)
                {
                    string substring = substrings[j];
                    float shakeAmount = intensities[j];

                    // Find the start index of the substring
                    int startIndex = fullText.IndexOf(substring);

                    if (startIndex == -1) // Substring not found, do nothing
                    {
                        Debug.LogWarning("Substring not found in the text.");
                        yield return null;
                    }

                    // Get the length of the substring
                    int substringLength = substring.Length;

                    // Iterate over each character
                    for (int i = 0; i < textInfo.characterCount; i++)
                    {
                        TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

                        // If the character is not visible, skip it
                        if (!charInfo.isVisible)
                            continue;

                        // Check if this character is within the range of the substring
                        if (i >= startIndex && i < startIndex + substringLength)
                        {
                            // Get the index of the material and character
                            int materialIndex = charInfo.materialReferenceIndex;
                            int vertexIndex = charInfo.vertexIndex;

                            // Get the vertices of the current character
                            Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

                            // Apply random shake to the character's position
                            Vector3 shakeOffset = new Vector3(
                                Random.Range(-shakeAmount, shakeAmount),
                                Random.Range(-shakeAmount, shakeAmount),
                                0);

                            // Offset each vertex of the character
                            vertices[vertexIndex + 0] += shakeOffset;
                            vertices[vertexIndex + 1] += shakeOffset;
                            vertices[vertexIndex + 2] += shakeOffset;
                            vertices[vertexIndex + 3] += shakeOffset;
                        }
                    }
                }

                // Update the mesh with vertex data on the correct TextMeshPro object
                for (int i = 0; i < textInfo.meshInfo.Length; i++)
                {
                    shakeText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
                }

                yield return new WaitForEndOfFrame();
            }
        }

       
        protected override void OnDestroy()
        {
            List<TMP_Text> list = new();
            shakeRoutines.Keys.CopyProperties(list);
            foreach (var item in list)
            {
                RemoveShake(item);
            }
        }
    }
}

