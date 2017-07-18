select distinct
    id
from
    (select distinct
        sistema as id,
        fabrica_teste as testManuf
    from
        alm_cts
    where
        sistema is not null and
        fabrica_teste is not null

    union all

    select distinct
        sistema_defeito as id,
        fabrica_teste as testManuf
    from
        alm_defeitos
    where
        sistema_defeito is not null and
        fabrica_teste is not null
    ) aux
where
    id <> '' and
    testManuf <> '' and
    testManuf in (@listTestManufs)
order by
    1