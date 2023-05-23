﻿using Model;
using Repository;

namespace RestDAOs;

public class RestAnimalDAO : IRestAnimalDAO
{
    private readonly DatabaseContext context;

    public RestAnimalDAO(DatabaseContext context)
    {
        this.context = context;
    }

    public async Task CreateAnimalAsync(Animal animal)
    {
        await context.Animals.AddAsync(animal);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAnimalAsync(string id)
    {
        Animal? existing = await context.Animals.FindAsync(Guid.Parse(id));

        if (existing == null)
            throw new Exception("Animal not found");

        context.Animals.Remove(existing);
        await context.SaveChangesAsync();
    }
}