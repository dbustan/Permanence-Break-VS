#ifndef DETECTEDGES_INCLUDED
#define DETECTEDGES_INCLUDED

static float sobelHMat[9] = {
    1,  2,  1,
    0,  0,  0,
    -1, -2, -1
};
static float sobelVMat[9] = {
    -1, 0, 1,
    -2, 0, 2,
    -1, 0, 1
};

void DepthEdges_float(float2 UV, float Thickness, out float Out) {

    float2 sobel = 0;
    float aspectRatio = (float)_ScreenParams.x / _ScreenParams.y;

    for(int y = 0; y < 3; y++) {
        for(int x = 0; x < 3; x++) {
            float depth = SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV + float2((x-1) / aspectRatio, y-1) * Thickness);
            int sobelIndex = y * 3 + x;
            sobel += depth * float2(sobelHMat[sobelIndex], sobelVMat[sobelIndex]);
        }
    }

    Out = length(sobel);
}
void ColorEdges_float(float UV, float Thickness, out float Out) {
    float2 sobelR = 0;
    float2 sobelG = 0;
    float2 sobelB = 0;

    [unroll] for(int y = 0; y < 3; y++) {
        [unroll] for(int x = 0; x < 3; x++) {
            float3 color = SHADERGRAPH_SAMPLE_SCENE_COLOR(UV + float2(x-1, y-1) * Thickness);
            int sobelIndex = y * 3 + x;
            float2 kernel = float2(sobelHMat[sobelIndex], sobelVMat[sobelIndex]);
            sobelR += color.x * kernel;
            sobelG += color.y * kernel;
            sobelB += color.z * kernel;
        }
    }

    Out = max(length(sobelR), max(length(sobelG), length(sobelB)));
}

#endif