/** @type {import('tailwindcss').Config} */
module.exports = {
    darkMode: 'class',

    content: [
        "./**/*.{razor,html,cshtml}"
    ],

    theme: {
        extend: {

            colors: {

                /* PRIMARY */
                primary: {
                    DEFAULT: '#635BFF',
                    dark: '#4F46E5',
                    light: '#8B83FF'
                },

                /* LIGHT MODE */
                background: '#F5F7FB',
                surface: '#FFFFFF',
                surfaceSoft: '#F8FAFC',

                textPrimary: '#0F172A',
                textMuted: '#64748B',

                borderColor: '#E2E8F0',

                sidebar: '#0B1120',

                /* DARK MODE */
                darkBackground: '#020617',
                darkSurface: '#0F172A',
                darkSurfaceSoft: '#111827',

                darkTextPrimary: '#F8FAFC',
                darkTextMuted: '#94A3B8',

                darkBorder: '#1E293B',
                darkSidebar: '#020617',
            },

            fontFamily: {
                sans: ['Inter', 'sans-serif']
            },

            boxShadow: {
                soft: '0 10px 30px rgba(15,23,42,0.06)',
                glow: '0 0 40px rgba(99,91,255,0.15)'
            },

            borderRadius: {
                xl2: '1rem',
                xl3: '1.5rem',
                xl4: '2rem'
            }
        },
    },

    plugins: [],
}