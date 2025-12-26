CREATE TABLE IF NOT EXISTS public.defaut_poste (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    id_post SMALLINT NOT NULL,
    defaut BOOLEAN NOT NULL,
    date_heure TIMESTAMP NOT NULL
);
CREATE TABLE IF NOT EXISTS public.production_piece (
    id_piece SERIAL PRIMARY KEY,
    date_prod TIMESTAMP NOT NULL,
    nb_pieces SMALLINT NOT NULL,
    id_post SMALLINT NOT NULL
);